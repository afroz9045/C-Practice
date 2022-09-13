using LibraryManagement.Core.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class PenaltiesController : ApiController
    {
        private readonly IPenaltyService _penaltyService;
        private readonly ILogger<PenaltiesController> _logger;

        public PenaltiesController(IPenaltyService penaltyService, ILogger<PenaltiesController> logger)
        {
            _penaltyService = penaltyService;
            _logger = logger;
        }

        [HttpPost("pay/{bookIssuedId}")]
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
    }
}