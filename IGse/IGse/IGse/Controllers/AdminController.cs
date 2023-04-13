using AutoMapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;
using IGse.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.Json;

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        private readonly IAdminRepository _adminRepository;

        public AdminController(IMapper mapper,ICustomerService customerService,IConfiguration configuration,IAdminRepository adminRepository)
        {
            _mapper = mapper;
            _customerService = customerService;
            _configuration = configuration;
            _adminRepository = adminRepository;
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegisterAdmin([FromBody] CustomerVm customerVm)
        {
            var mappedCustomer = _mapper.Map<CustomerVm, Customers>(customerVm);
            var addedCustomer = await _customerService.AddCustomerAsync(mappedCustomer);
            UserVm user = new UserVm();
            user.Email = customerVm.EmailId;
            user.role = "Admin";
            user.Password = customerVm.Password;
            user.CustomerId = addedCustomer.CustomerId;
            user.FullName = addedCustomer.Name;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetSection("Constants").GetSection("AuthenticationBaseUrl").Value!);
                var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
                var result = await client.PostAsync(_configuration.GetSection("Constants").GetSection("AuthenticationSubUrl").Value, content);
            }
            if (addedCustomer is not null)
                return Ok(addedCustomer);
            return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult> GetAdmins()
        {
            var admins = await _adminRepository.GetAdmins();
            if(admins.Any())
                return Ok(admins);
            return NotFound("No Admin Exits!");
        }

    }
}
