using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStudentRepository _studentRepository;

        public IssueService(IIssueRepository issueRepository, IBookRepository bookRepository, IStaffRepository staffRepository, IStudentRepository studentRepository)
        {
            _issueRepository = issueRepository;
            _bookRepository = bookRepository;
            _staffRepository = staffRepository;
            _studentRepository = studentRepository;
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
                        issuedBook.ExpiryDate = DateTime.Today.AddDays(30);
                        issuedBook.StaffId = issue.StaffId;
                        issuedBook.StudentId = issue.StudentId;

                        bookIdResult.StockAvailable -= 1;
                        return issuedBook;
                    }
                }
            }
            return null;
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