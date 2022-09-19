using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [HttpPost("pay/{bookIssuedId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PayPenalty(short bookIssuedId, [FromBody] int penaltyAmount)
        {
            var existingPenalty = await _penaltyRepository.GetPenaltyByIdAsync(bookIssuedId);
            var bookIssuedDetails = await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId);
            _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
            var isPenaltyExist = await _penaltyRepository.IsPenalty(bookIssuedId, existingPenalty, bookIssuedDetails);
            if (isPenaltyExist == null)
            {
                return BadRequest("Penalty not found!");
            }
            var penaltyPaidStatusDetails = _penaltyService.PayPenalty(bookIssuedId, penaltyAmount, isPenaltyExist, bookIssuedDetails);
            if (penaltyPaidStatusDetails != null && penaltyPaidStatusDetails.PenaltyPaidStatus == true)
            {
                var penaltyPaid = _penaltyRepository.PayPenaltyAsync(penaltyPaidStatusDetails);
                _logger.LogInformation($"Paying Penalty with book issued id: {bookIssuedId}");
                return Ok("Transaction is successful");
            }
            return NotFound("Transaction Failed");
        }
    }
}