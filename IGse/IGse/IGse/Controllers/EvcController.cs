using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IGse.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class EvcController : ControllerBase
    {
        private readonly IEvcRepository _evcRepository;
        private readonly IEvcService _evcService;
        private readonly ICustomerEvcHistoryRepository _customerEvcHistoryRepository;

        public EvcController(IEvcRepository evcRepository,IEvcService evcService,ICustomerEvcHistoryRepository customerEvcHistoryRepository)
        {
            _evcRepository = evcRepository;
            _evcService = evcService;
            _customerEvcHistoryRepository = customerEvcHistoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetEvcs()
        {
            var evcResult = await _evcRepository.GetEvcsAsync();
            if (evcResult.Any())
                return Ok(evcResult);
            return NotFound();
        }

        [HttpGet("/voucher")]
        public async Task<ActionResult> GetEvcByVoucher([MaxLength(8)] string evcVoucher)
        {
            var evc = await _evcRepository.GetEvcByVoucher(evcVoucher);
            if(evc is not null)
                return Ok(evc);
            return BadRequest("Evc not found!");
        }

        [HttpGet("/evc-history/{customerId}")]
        public async Task<ActionResult> GetCustomerEvcHistory([Required] int customerId)
        {
            var evcHistory = await _customerEvcHistoryRepository.GetCustomerEvcHistoryByCustomerId(customerId);
            if(evcHistory is not null)
                return Ok(evcHistory);
            return NotFound("No history found!");
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetEvcById(int id)
        {
            var evc = await _evcRepository.GetEvcByIdAsync(id);
            if(evc is not null)
                return Ok(evc);
            return NotFound();
        }

        [HttpPost("/subsidy/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEvc()
        {
            var addedEvc = await _evcService.AddEvcAsync();
            if(!string.IsNullOrEmpty(addedEvc))
                return Ok(addedEvc);
            return BadRequest("An error occured while generating Evc!");
        }
        [HttpGet("subsidyEvc")]
        public async Task<ActionResult> GetSubsidyEvc()
        {
            var subsidyEvc = await _evcRepository.GetSubsidyEvc();
            if (subsidyEvc is null)
                return NotFound("Subsidy Evc not found!");
            return Ok(subsidyEvc);
        }
        [HttpPost("/add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddEvc([FromBody,Required] int amount)
        {
            var addedEvc = await _evcService.AddCustomEvc(amount);
            if (!string.IsNullOrEmpty(addedEvc))
                return Ok(addedEvc);
            return BadRequest("An error occured while generating Evc!");
        }

        [HttpGet("validate")]
        public async Task<ActionResult> ValidateEvc([MaxLength(8)] string evcVoucher)
        {
            var isValidEvc = await _evcService.ValidateEvc(evcVoucher);
            if (isValidEvc)
                return Ok(isValidEvc);
            return BadRequest("Invalid Evc Voucher!");
        }

        [HttpPost("mark-evc-as-used")]
        public async Task<ActionResult> MarkEvcAsUsed([Required] int customerId, [Required,MaxLength(8)] string evc)
        {
            var isValidEvc = await _evcService.ValidateEvc(evc);
            if (isValidEvc)
            {
                var evcRecord = await _evcRepository.GetEvcByVoucher(evc);
                var evcUsed = await _evcService.MarkEvcAsUsed(customerId,evcRecord);
                if(evcUsed.IsUsed)
                    return Ok(true);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
    }
}
