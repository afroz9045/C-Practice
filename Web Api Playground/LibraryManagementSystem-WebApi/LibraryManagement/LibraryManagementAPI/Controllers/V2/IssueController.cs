using AutoMapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    public class IssueController : ApiController
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueController> _logger;

        public IssueController(IIssueRepository issueRepository, IIssueService issueService, IMapper mapper, ILogger<IssueController> logger)
        {
            _issueRepository = issueRepository;
            _issueService = issueService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("staff/{bookIssuedToStaff}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] string bookIssuedToStaff)
        {
            _logger.LogInformation($"Getting Book issued to staff with staff id: {bookIssuedToStaff}");
            var bookIssuedToRecords = await _issueService.GetBookIssuedToEntityDetails(studentId: 0, staffId: bookIssuedToStaff);
            if (bookIssuedToRecords != null)
            {
                return Ok(bookIssuedToRecords);
            }
            return BadRequest("Data not Found!");
        }

        [HttpGet("students/{bookIssuedToStudent}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] int bookIssuedToStudent)
        {
            _logger.LogInformation($"Getting Book issued to student with student id: {bookIssuedToStudent}");
            var bookIssuedToRecords = await _issueService.GetBookIssuedToEntityDetails(studentId: bookIssuedToStudent, staffId: null);
            if (bookIssuedToRecords != null)
            {
                return Ok(bookIssuedToRecords);
            }
            return BadRequest("Data not Found!");
        }
    }
}