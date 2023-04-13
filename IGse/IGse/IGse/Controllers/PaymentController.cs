using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IEvcService _evcService;
        private readonly IEvcRepository _evcRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentService _paymentService;

        public PaymentController(IBillRepository billRepository, IEvcService evcService, IEvcRepository evcRepository,ICustomerRepository customerRepository,IPaymentService paymentService)
        {
            _billRepository = billRepository;
            _evcService = evcService;
            _evcRepository = evcRepository;
            _customerRepository = customerRepository;
            _paymentService = paymentService;
        }

        [HttpPost("paybill")]
        public async Task<ActionResult> PayBill([Required] int billId, [FromBody, Required] int amountToPay)
        {
          var existingBill = await _billRepository.GetBillByBillIdAsync(billId);
            if (existingBill.Amount <= 0)
                return BadRequest("Insufficient amount to pay!");
            if (existingBill == null)
                return BadRequest("Bill not found!");
            if (existingBill is not null && existingBill.IsPaid)
                return BadRequest("Bill already paid!");
            if(existingBill is not null && existingBill.Amount == amountToPay)
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(existingBill.CustomerId);
                var paymentStatus = await _paymentService.PayBillAsync(existingBill,customer);
                if (paymentStatus != null)
                    return Ok(paymentStatus);
            }
            return BadRequest("Payment failed!");
        }
    }

}

