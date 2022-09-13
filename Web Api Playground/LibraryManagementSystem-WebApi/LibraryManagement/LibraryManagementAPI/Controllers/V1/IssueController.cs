using AutoMapper;
using EmployeeRecordBook.Api.Infrastructure.Specs;
using LibraryManagement.Api.ViewModels;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    [ApiVersion("1.0")]
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

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddIssueBook([FromBody] IssueVm issueVm)
        {
            if (issueVm.StudentId != null && issueVm.StaffId == null)
                _logger.LogInformation($"Adding Book issue details with student id: {issueVm.StudentId}");
            _logger.LogInformation($"Adding Book issue details with staff id: {issueVm.StaffId}");
            var issuedBook = _mapper.Map<IssueVm, Issue>(issueVm);
            var bookIssueResult = await _issueService.AddBookIssueAsync(issuedBook);
            if (bookIssueResult.IssueId != 0)
            {
                return Ok(bookIssueResult);
            }
            return NotFound("Sorry, book not found!");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssueBookDetails()
        {
            _logger.LogInformation("Getting books issued details");
            var booksIssued = await _issueService.GetBookIssuedAsync();
            if (booksIssued != null)
                return Ok(booksIssued);
            return NotFound();
        }

        [HttpGet("{bookIssuedId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssueBookByIdDetails(short bookIssuedId)
        {
            _logger.LogInformation($"Getting book issued details by book id: {bookIssuedId} ");
            var bookIssued = await _issueService.GetBookIssuedByIdAsync(bookIssuedId);
            if (bookIssued != null)
                return Ok(bookIssued);
            return NotFound();
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

        [HttpPut("{bookIssuedId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateIssuedBookDetails(short bookIssuedId, [FromBody] Issue issue)
        {
            _logger.LogInformation($"Updating book issued details with book issued id: {bookIssuedId}");
            var result = await _issueService.UpdateBookIssuedAsync(bookIssuedId, issue);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{bookIssuedId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteIssue(short bookIssuedId)
        {
            _logger.LogInformation($"Deleting book issed details with book issued id: {bookIssuedId}");
            var issueDelete = await _issueService.DeleteIssueAsync(bookIssuedId);
            if (issueDelete != null)
                return Ok(issueDelete);
            return NotFound();
        }
    }
}