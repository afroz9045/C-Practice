using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.Core.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IConfiguration _configuration;

        public IssueService(IIssueRepository issueRepository, IBookRepository bookRepository, IStaffRepository staffRepository, IStudentRepository studentRepository, IConfiguration configuration)
        {
            _issueRepository = issueRepository;
            _bookRepository = bookRepository;
            _staffRepository = staffRepository;
            _studentRepository = studentRepository;
            _configuration = configuration;
        }

        public Issue? AddBookIssueAsync(Issue issue, Book? bookIdResult, Staff? staffIdValidate, Student? studentIdValidate)
        {
            var issuedBook = new Issue();
            if (issue.StaffId != null || issue.StudentId != null)
            {
                issuedBook.BookId = issue.BookId;

                if (staffIdValidate != null || studentIdValidate != null)
                {
                    if (bookIdResult != null && bookIdResult.StockAvailable > 0)
                    {
                        issuedBook.IssueDate = DateTime.Today;
                        issuedBook.ExpiryDate = DateTime.Today.AddDays(/*30*/Convert.ToInt32(_configuration.GetSection("Constants").GetSection("BookIssueDays").Value));
                        issuedBook.StaffId = issue.StaffId;
                        issuedBook.StudentId = issue.StudentId;

                        bookIdResult.StockAvailable -= 1;
                        return issuedBook;
                    }
                }
            }
            return null;
        }

        public IEnumerable<IssueDto> IsStudentOrStaff(IEnumerable<Issue> issue)
        {
            List<IssueDto> result = new List<IssueDto>();
            foreach (var issuedRecord in issue)
            {
                IssueDto issueDto = new();
                issueDto.BookId = issuedRecord.BookId;
                issueDto.ExpiryDate = issuedRecord.ExpiryDate;
                issueDto.IssueDate = issuedRecord.IssueDate;
                issueDto.IssueId = issuedRecord.IssueId;
                issueDto.Id = string.IsNullOrEmpty(issuedRecord.StaffId) ? issuedRecord.StudentId.ToString() : issuedRecord.StaffId;
                issueDto.IssuedTo = string.IsNullOrEmpty(issuedRecord.StaffId) ? "Student" : "Staff";
                result.Add(issueDto);
            }
            return result;
        }

        public Issue? UpdateBookIssuedAsync(short issueId, Issue existingIssue, Issue issue)
        {
            existingIssue.IssueId = issueId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.ExpiryDate = issue.ExpiryDate;

            return existingIssue;
        }
    }
}