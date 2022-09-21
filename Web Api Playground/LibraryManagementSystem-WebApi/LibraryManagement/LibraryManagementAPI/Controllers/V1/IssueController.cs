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
        private readonly IBookRepository _bookRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueController> _logger;

        public IssueController(IIssueRepository issueRepository, IBookRepository bookRepository, IStaffRepository staffRepository, IStudentRepository studentRepository, IIssueService issueService, IMapper mapper, ILogger<IssueController> logger)
        {
            _issueRepository = issueRepository;
            _bookRepository = bookRepository;
            _staffRepository = staffRepository;
            _studentRepository = studentRepository;
            _issueService = issueService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> AddIssueBook([FromBody] IssueVm issueVm)
        {
            var bookIssuedDetails = await _issueRepository.GetBookIssuedByBookId(issueVm.BookId);
            if (bookIssuedDetails != null)
            {
                foreach (var bookIssuedData in bookIssuedDetails)
                {
                    if (((bookIssuedData.StudentId != 0 && bookIssuedData.StudentId == issueVm.StudentId) || (bookIssuedData.StaffId != null && bookIssuedData.StaffId == issueVm.StaffId)) && (bookIssuedData.BookId == issueVm.BookId))
                    {
                        return BadRequest($"Book with book id {issueVm.BookId} is already issued!");
                    }
                }
            }
            var issuedBook = _mapper.Map<IssueVm, Issue>(issueVm);
            var bookIdResult = await _bookRepository.GetBookById(issuedBook.BookId);
            var staffIdValidate = await _staffRepository.GetStaffByIdAsync(issueVm.StaffId);
            var studentIdValidate = await _studentRepository.GetStudentByIdAsync(issueVm.StudentId ?? 0);
            var bookToBeIssue = _issueService.AddBookIssueAsync(issuedBook, bookIdResult, staffIdValidate, studentIdValidate);
            var bookIssuedResult = await _issueRepository.AddBookIssueAsync(bookToBeIssue, bookIdResult);

            if (bookIssuedResult != null && bookIssuedResult.IssueId != 0)
            {
                if (bookIssuedResult.StudentId >= 0)
                {
                    _logger.LogInformation($"Adding Book issue details with student id: {issueVm.StudentId}");
                    return Ok(bookIssuedResult);
                }
                _logger.LogInformation($"Adding Book issue details with staff id: {issueVm.StaffId}");
                return Ok(bookIssuedResult);
            }
            return BadRequest("Sorry, book is not added");
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssueBookDetails()
        {
            _logger.LogInformation("Getting books issued details");
            var booksIssued = await _issueRepository.GetBookIssuedAsync();
            if (booksIssued != null)
                return Ok(booksIssued);
            return NotFound();
        }

        [HttpGet("{bookIssuedId:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssueBookByIdDetails(short bookIssuedId)
        {
            _logger.LogInformation($"Getting book issued details by book id: {bookIssuedId} ");
            var bookIssued = await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId);
            if (bookIssued != null)
                return Ok(bookIssued);
            return NotFound();
        }

        [HttpGet("staff/{bookIssuedToStaff}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
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

        [HttpGet("students/{bookIssuedToStudent:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] int bookIssuedToStudent)
        {
            if (bookIssuedToStudent <= 0)
            {
                return BadRequest("Invalid student id");
            }
            _logger.LogInformation($"Getting Book issued to student with student id: {bookIssuedToStudent}");
            var bookIssuedToRecords = await _issueRepository.GetBookIssuedToEntityDetails(studentId: bookIssuedToStudent);
            if (bookIssuedToRecords != null)
            {
                return Ok(bookIssuedToRecords);
            }
            return BadRequest("Data not Found!");
        }

        [HttpPut("{bookIssuedId}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> UpdateIssuedBookDetails(short bookIssuedId, [FromBody] IssueUpdateVm updateIssue)
        {
            var existingBookIssuedRecord = await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId);
            if (existingBookIssuedRecord == null)
            {
                return BadRequest($"Book issued with issued id {bookIssuedId} is not exist!");
            }
            _logger.LogInformation($"Updating book issued details with book issued id: {bookIssuedId}");
            var updateIssueMapped = _mapper.Map<IssueUpdateVm, Issue>(updateIssue);
            var bookIssueToBeUpdate = _issueService.UpdateBookIssuedAsync(bookIssuedId, existingBookIssuedRecord, updateIssueMapped);
            var updatedBookIssue = _issueRepository.UpdateBookIssuedAsync(bookIssueToBeUpdate);
            if (updatedBookIssue != null)
                return Ok(bookIssueToBeUpdate);
            return BadRequest();
        }

        [HttpDelete("{bookIssuedId}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task<ActionResult> DeleteIssue(short bookIssuedId)
        {
            var issuedRecord = await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId);
            if (issuedRecord == null)
            {
                BadRequest("Book Issue not found!");
            }
            var deletedBookIssued = await _issueRepository.DeleteIssueAsync(issuedRecord!);
            _logger.LogInformation($"Deleting book issued details with book issued id: {bookIssuedId}");
            if (deletedBookIssued != null)
                return NoContent();
            return NotFound();
        }
    }
}