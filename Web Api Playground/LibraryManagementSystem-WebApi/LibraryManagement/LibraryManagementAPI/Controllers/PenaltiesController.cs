using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class PenaltiesController : ApiController
    {
        private readonly IPenaltyService _penaltyService;
        private readonly ILogger<PenaltiesController> _logger;
        private object penalties;

        public PenaltiesController(IPenaltyService penaltyService, ILogger<PenaltiesController> logger)
        {
            _penaltyService = penaltyService;
            _logger = logger;
        }

        [HttpPost("PayPenalty/{bookIssuedId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PayPenalty(short bookIssuedId, [FromBody] int penaltyAmount)
        {
            _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
            var penaltyPaidStatus = await _penaltyService.PayPenaltyAsync(bookIssuedId, penaltyAmount);
            if (penaltyPaidStatus == true)
            {
                return Ok("Transaction is successfull");
            }
            return NotFound("Transaction Failed");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetPenalties()
        {
            _logger.LogInformation("Geting Penalties}");
            var penalties = await _penaltyService.GetPenaltiesAsync();
            if (penalties != null)
            {
                return Ok(penalties);
            }
            return NotFound("Penalties not Found!");
        }

        [HttpGet("{issueId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetPenaltiesById(short issueId)
        {
            _logger.LogInformation($"Getting penalty with issue book issue id: {issueId}");
            var penalty = await _penaltyService.GetPenaltyByIdAsync(issueId);
            if (penalty != null)
            {
                return Ok(penalty);
            }
            return NotFound("Penalty not Found!");
        }

        [HttpDelete("{issueId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeletePenalty(short issueId)
        {
            _logger.LogInformation($"Deleting penalty with book issue id: {issueId} ");
            var deletedPenalty = await _penaltyService.DeletePenaltyAsync(issueId);
            if (deletedPenalty != null)
            {
                return Ok(deletedPenalty);
            }
            return BadRequest();
        }
    }
}