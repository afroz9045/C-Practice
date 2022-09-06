using AutoMapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagementAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    public class IssueController : ApiController
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;

        public IssueController(IIssueRepository issueRepository, IMapper mapper)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddIssueBook([FromBody] IssueVm issueVm)
        {
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
            return Ok(await _issueRepository.GetBookIssuedAsync());
        }

        [HttpGet("{bookIssuedId}")]
        public async Task<ActionResult> GetIssueBookByIdDetails(short bookIssuedId)
        {
            return Ok(await _issueRepository.GetBookIssuedByIdAsync(bookIssuedId));
        }

        [HttpGet("staff/{bookIssuedToStaff}")]
        public async Task<ActionResult> GetIssuedBooksDetails([FromRoute] string bookIssuedToStaff)
        {
            //var issuedBookTo = _mapper.Map<BookIssuedToVm, BookIssuedTo>(bookIssuedTo);
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
            //var issuedBookTo = _mapper.Map<BookIssuedToVm, BookIssuedTo>(bookIssuedTo);
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
            var result = Ok(await _issueRepository.UpdateBookIssuedAsync(bookIssuedId, issue));
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        [HttpDelete("{issueId}")]
        public async Task<ActionResult> DeleteIssue(short issueid)
        {
            var issueDelete = await _issueRepository.DeleteIssueAsync(issueid);
            if (issueDelete != null)
                return Ok(issueDelete);
            return NotFound();
        }
    }
}