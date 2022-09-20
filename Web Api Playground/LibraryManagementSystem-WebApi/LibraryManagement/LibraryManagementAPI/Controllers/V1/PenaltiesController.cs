using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
    public class PenaltiesController : ApiController
    {
        private readonly IPenaltyService _penaltyService;
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly ILogger<PenaltiesController> _logger;

        public PenaltiesController(IPenaltyService penaltyService, IPenaltyRepository penaltyRepository, IIssueRepository issueRepository, ILogger<PenaltiesController> logger)
        {
            _penaltyService = penaltyService;
            _penaltyRepository = penaltyRepository;
            _issueRepository = issueRepository;
            _logger = logger;
        }

        [HttpPost("pay/{bookIssuedId},{penaltyAmount}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PayPenalty(short bookIssuedId, int penaltyAmount)
        {
            var existingPenalty = await _penaltyRepository.GetPenaltyByIdAsync(bookIssuedId);
            var bookIssuedDetails = await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId);
            _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
            Penalty? isPenalty = _penaltyService.IsPenalty(bookIssuedId, existingPenalty, bookIssuedDetails);
            if (isPenalty == null)
            {
                //throw new ArgumentException();
                return BadRequest("Penalty not found!");
            }
            var isPenaltyExist = await _penaltyRepository.IsPenalty(isPenalty);
            var penaltyPaidStatusDetails = isPenaltyExist != null ? _penaltyService.PayPenalty(penaltyAmount, isPenaltyExist) : null;
            if (penaltyPaidStatusDetails != null && penaltyPaidStatusDetails.PenaltyPaidStatus == true)
            {
                var penaltyPaid = _penaltyRepository.PayPenaltyAsync(penaltyPaidStatusDetails);
                _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
                return Ok("Transaction is successful");
            }
            return NotFound("Transaction Failed");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetPenalties()
        {
            _logger.LogInformation("Getting Penalties}");
            var penalties = await _penaltyRepository.GetPenaltiesAsync();
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
            var penalty = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
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
            var penaltyToBeDelete = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            if (penaltyToBeDelete != null)
            {
                var deletedPenalty = await _penaltyRepository.DeletePenaltyAsync(penaltyToBeDelete);
                return Ok(deletedPenalty);
            }
            return BadRequest("Penalty not found!");
        }
    }
}