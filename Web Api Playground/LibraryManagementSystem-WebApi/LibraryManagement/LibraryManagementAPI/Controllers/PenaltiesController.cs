using LibraryManagement.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class PenaltiesController : ApiController
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly ILogger<PenaltiesController> _logger;
        private object penalties;

        public PenaltiesController(IPenaltyRepository penaltyRepository, ILogger<PenaltiesController> logger)
        {
            _penaltyRepository = penaltyRepository;
            _logger = logger;
        }

        [HttpPost("PayPenalty/{bookIssuedId}")]
        public async Task<ActionResult> PayPenalty(short bookIssuedId, [FromBody] int penaltyAmount)
        {
            _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
            var penaltyPaidStatus = await _penaltyRepository.PayPenaltyAsync(bookIssuedId, penaltyAmount);
            if (penaltyPaidStatus == true)
            {
                return Ok("Transaction is successfull");
            }
            return NotFound("Transaction Failed");
        }

        [HttpGet]
        public async Task<ActionResult> GetPenalties()
        {
            _logger.LogInformation("Geting Penalties}");
            var penalties = await _penaltyRepository.GetPenaltiesAsync();
            if (penalties != null)
            {
                return Ok(penalties);
            }
            return NotFound("Penalties not Found!");
        }

        [HttpGet("{issueId}")]
        public async Task<ActionResult> GetPenaltiesById(short issueId)
        {
            _logger.LogInformation($"Getting penalty with issue book issue id: {issueId}");
            var penalty = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            if (penalty != null)
            {
                return Ok(penalty);
            }
            return NotFound("Penalty not Found!");
        }

        [HttpDelete("{issueId}")]
        public async Task<ActionResult> DeletePenalty(short issueId)
        {
            _logger.LogInformation($"Deleting penalty with book issue id: {issueId} ");
            var deletedPenalty = await _penaltyRepository.DeletePenaltyAsync(issueId);
            if (deletedPenalty != null)
            {
                return Ok(deletedPenalty);
            }
            return BadRequest();
        }
    }
}