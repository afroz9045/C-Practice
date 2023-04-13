using AutoMapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;
using IGse.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;
        private readonly IBillRepository _billRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public BillController(IBillService billService,IBillRepository billRepository,ICustomerRepository customerRepository,IMapper mapper)
        {
            _billService = billService;
            _billRepository = billRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpGet("bills")]
        public async Task<ActionResult> GetBills()
        {
            var bills = await _billService.GetBillsAsync();
            if(bills.Any())
                return Ok(bills);
            return NotFound();
        }

        [HttpGet("bills/customer/{customerId}")]
        public async Task<ActionResult> GetBillsByCustomerId([Required] int customerId)
        {
            var customer = _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer is null)
                return BadRequest("Invalid customer!");
            var bills = await _billRepository.GetBillsForCustomerId(customerId);
            if(bills.Any())
                return Ok(bills);
            return NotFound("Bills not exists!");
        }

        [HttpGet("/{billId}")]
        public async Task<ActionResult> GetByByBillId([Required] int billId)
        {
            var bill = await _billRepository.GetBillByBillIdAsync(billId);
            if(bill is not null)
                return Ok(bill);
            return NotFound("Bill not found!");
        }

        [HttpPost]
        public async Task<ActionResult> GetBill([FromBody,Required]ReadingsVm readingsVm)
        {   
            var customer = await _customerRepository.GetCustomerByIdAsync(readingsVm.CustomerId);
            if (customer is null)
                return BadRequest("Customer not found!");
            var customerBills = await _billRepository.GetBillsForCustomerId(readingsVm.CustomerId);
            var mappedBill = _mapper.Map<ReadingsVm, Bill>(readingsVm);
            mappedBill.BillMonthYear = readingsVm.BillMonthYear;
            if ((mappedBill.BillMonthYear.Month > DateTime.UtcNow.Month) && (mappedBill.BillMonthYear.Year > DateTime.UtcNow.Year))
            {
                return BadRequest("Invalid month or year!");
            }
            var isBillExistForMonth = customerBills.FirstOrDefault(x=>x.BillMonthYear.Month ==mappedBill.BillMonthYear.Month);
            if (isBillExistForMonth is not null)
                return Ok(isBillExistForMonth);
            var bill = await _billService.GetBillAsync(mappedBill);
            if (bill is not null)
                return Ok(bill);
            if (bill is null)
                return BadRequest("Please pay previous outstanding bills!");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
