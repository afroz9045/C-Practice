using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class IssueController : ApiController
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueController> _logger;

        public IssueController(IIssueRepository issueRepository, IMapper mapper, ILogger<IssueController> logger)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> AddIssueBook([FromBody] IssueVm issueVm)
        {
            if (issueVm.StudentId != null && issueVm.StaffId == null)
                _logger.LogInformation($"Adding Book issue details with student id: {issueVm.StudentId}");
            _logger.LogInformation($"Adding Book issue details with staff id: {issueVm.StaffId}");
            var issuedBook = _mapper.Map<IssueVm, Issue>(issueVm);
            var bookIssueResult = await _issueRepository.AddBookIssueAsync(issuedBook);
            if (bookIssueResult.IssueId != 0)
            {
                return Ok(bookIssueResult);
            }
            return NotFound("Sorry, book not found!");
        }

        [HttpGet]
        public async Task<ActionResult> GetIssueBookDetails()
        {
            _logger.LogInformation("Getting books issued details");
            return Ok(await _issueRepository.GetBookIssuedAsync());
        }

        [HttpGet("{bookIssuedId}")]
        public async Task<ActionResult> GetIssueBookByIdDetails(short bookIssuedId)
        {
            _logger.LogInformation($"Getting book issued details by book id: {bookIssuedId} ");
            return Ok(await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId));
        }

        [HttpGet("staff/{bookIssuedToStaff}")]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] string bookIssuedToStaff)
        {
            _logger.LogInformation($"Getting Book issued to staff with staff id: {bookIssuedToStaff}");
            var bookIssuedToRecords = await _issueRepository.GetBookIssuedToEntityDetails(studentId: 0, staffId: bookIssuedToStaff);
            if (bookIssuedToRecords != null)
            {
                return Ok(bookIssuedToRecords);
            }
            return BadRequest("Data not Found!");
        }

        [HttpGet("students/{bookIssuedToStudent}")]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] int bookIssuedToStudent)
        {
            _logger.LogInformation($"Getting Book issued to student with student id: {bookIssuedToStudent}");
            var bookIssuedToRecords = await _issueRepository.GetBookIssuedToEntityDetails(studentId: bookIssuedToStudent, staffId: null);
            if (bookIssuedToRecords != null)
            {
                return Ok(bookIssuedToRecords);
            }
            return BadRequest("Data not Found!");
        }

        [HttpPut("{bookIssuedId}")]
        public async Task<ActionResult> UpdateIssuedBookDetails(short bookIssuedId, [FromBody] Issue issue)
        {
            _logger.LogInformation($"Updating book issued details with book issued id: {bookIssuedId}");
            var result = Ok(await _issueRepository.UpdateBookIssuedAsync(bookIssuedId, issue));
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{issueId}")]
        public async Task<ActionResult> DeleteIssue(short issueid)
        {
            _logger.LogInformation($"Deleting book issed details with book issued id: {issueid}");
            var issueDelete = await _issueRepository.DeleteIssueAsync(issueid);
            if (issueDelete != null)
                return Ok(issueDelete);
            return NotFound();
        }
    }
}