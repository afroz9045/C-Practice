using LibraryManagement.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class PenaltiesController : ApiController
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private object penalties;

        public PenaltiesController(IPenaltyRepository penaltyRepository)
        {
            _penaltyRepository = penaltyRepository;
        }

        [HttpPost("{bookIssuedId}")]
        public async Task<ActionResult> PayPenalty(short bookIssuedId, [FromBody] int penaltyAmount)
        {
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
            var deletedPenalty = await _penaltyRepository.DeletePenaltyAsync(issueId);
            if (deletedPenalty != null)
            {
                return Ok(deletedPenalty);
            }
            return BadRequest();
        }
    }
}