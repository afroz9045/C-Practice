using AutoMapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Dtos;
using IGse.Core.Entities;
using IGse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json;

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IEvcRepository _evcRepository;
        private readonly IEvcService _evcService;
        private readonly ICustomerEvcHistoryRepository _customerEvcHistoryRepository;

        public CustomerController(ICustomerRepository customerRepository, ICustomerService customerService, IMapper mapper, IConfiguration configuration, IEvcRepository evcRepository, IEvcService evcService, ICustomerEvcHistoryRepository customerEvcHistoryRepository)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _mapper = mapper;
            _configuration = configuration;
            _evcRepository = evcRepository;
            _evcService = evcService;
            _customerEvcHistoryRepository = customerEvcHistoryRepository;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            if (!(customers.Any()))
                return NotFound();
            return Ok(customers);
        }
        [HttpGet("{customerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetCustomerById([Required] int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer is not null)
                return Ok(customer);
            return NotFound();
        }
        [HttpGet("/wallet-amount/{customerId}")]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> GetWalletAmount([Required] int customerId)
        {
            var walletAmount = await _customerRepository.GetWalletAmountForCustomerId(customerId);
            if (walletAmount is 0)
                return Ok(0);
            return Ok(walletAmount);
        }

        [HttpPost("/add-or-update")]
        public async Task<IActionResult> AddOrUpdateWalletAmount([Required] int customerId, [Required,MaxLength(8)] string evc)
        {
            bool isValidEvc;
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer is null)
                return BadRequest("Invalid customer!");
            var existingEvc = await _evcRepository.GetEvcByVoucher(evc);
            if (existingEvc is null)
                return BadRequest("Invalid Evc!");
            isValidEvc = await _evcService.ValidateEvc(evc);
            if(existingEvc is not null && isValidEvc)
            {
                var isCustomerWalletUpdated = await _customerService.AddOrUpdateWalletAmount(customer, existingEvc.Amount);
                if (isCustomerWalletUpdated)
                {
                    existingEvc.IsUsed = true;
                    existingEvc.UsedByCustomer = customerId;
                    await _evcRepository.UpdateEvcAsync(existingEvc);
                    var updatedCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
                    CustomerEvcHistory customerEvcHistory = new CustomerEvcHistory()
                    {
                        CustomerId = customerId,
                        DateOfUsed = DateTime.UtcNow,
                        EvcId= existingEvc.EvcId
                    };
                    await _customerEvcHistoryRepository.AddCustomerEvcHistory(customerEvcHistory);
                    return Ok(updatedCustomer.WalletAmount);
                }
            }
            
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> AddCustomer([FromBody] CustomerVm customerVm)
        {
            var mappedCustomer = _mapper.Map<CustomerVm, Customers>(customerVm);
            Evc? evcRecord = null;
            var addedCustomer = await _customerService.AddCustomerAsync(mappedCustomer);
            if (addedCustomer is not null)
            {
                if (customerVm.Evc is not null)
                {
                    evcRecord = await _evcRepository.GetEvcByVoucher(customerVm.Evc);
                    var isValidEvc = await _evcService.ValidateEvc(customerVm.Evc);
                    if (evcRecord is not null && addedCustomer is not null && isValidEvc)
                    {
                        addedCustomer.WalletAmount += evcRecord.Amount;
                        var evc = await _evcRepository.GetEvcByVoucher(evcRecord.EvcVoucher);
                        evc.UsedByCustomer = addedCustomer.CustomerId;
                        evc.IsUsed = true;
                        await _evcRepository.UpdateEvcAsync(evc);
                        await _customerRepository.UpdateCustomer(addedCustomer);
                        CustomerEvcHistory evcHistory = new CustomerEvcHistory()
                        {
                            CustomerId = addedCustomer.CustomerId,
                            DateOfUsed = DateTime.UtcNow,
                            EvcId = evcRecord.EvcId
                        };
                        await _customerEvcHistoryRepository.AddCustomerEvcHistory(evcHistory);
                    }
                }
                if (addedCustomer is not null)
                {
                    UserVm user = new UserVm();
                    user.Email = customerVm.EmailId;
                    user.role = "Customer";
                    user.Password = customerVm.Password;
                    user.CustomerId = addedCustomer.CustomerId;
                    user.FullName = addedCustomer.Name;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetSection("Constants").GetSection("AuthenticationBaseUrl").Value!);
                        var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                        var result = await client.PostAsync(_configuration.GetSection("Constants").GetSection("AuthenticationSubUrl").Value, content);
                    }
                    return Ok(addedCustomer);
                }
               
                
            }
            
            
            return BadRequest();
        }
        [HttpPut]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<ActionResult> UpdateCustomer([FromQuery, Required] int id, CustomerUpdateVm customerUpdateVm)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer is null)
                return BadRequest("Customer not found!");
            var mappedCustomer = _mapper.Map<CustomerUpdateVm, Customers>(customerUpdateVm);
            mappedCustomer.CustomerId = id;
            mappedCustomer.WalletAmount = existingCustomer.WalletAmount;
            var isCustomerUpdated = await _customerRepository.UpdateCustomer(mappedCustomer);
            if (isCustomerUpdated)
                return Ok(isCustomerUpdated);
            return BadRequest();
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCustomer([FromQuery, Required] int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
            if (existingCustomer is null)
                return BadRequest("Customer not found!");
            var isCustomerDeleted = await _customerRepository.DeleteCustomer(existingCustomer);
            if (isCustomerDeleted)
                return Ok(isCustomerDeleted);
            return BadRequest();
        }
    }
}
