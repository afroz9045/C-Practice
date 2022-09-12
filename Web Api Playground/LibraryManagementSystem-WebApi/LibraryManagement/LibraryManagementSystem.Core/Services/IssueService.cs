using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Dtos;
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

        public async Task<Issue?> AddBookIssueAsync(Issue issue)
        {
            var issuedBook = new Issue();
            if (issue.StaffId != null || issue.StudentId != null)
            {
                issuedBook.BookId = issue.BookId;
                var bookIdResult = await _bookRepository.GetBookById(issuedBook.BookId);
                var staffIdValidate = await _staffRepository.GetStaffByIdAsync(issue.StaffId);
                var studentIdValidate = await _studentRepository.GetStudentByIdAsync(issue.StudentId ?? 0);
                if (staffIdValidate != null || studentIdValidate != null)
                {
                    if (bookIdResult != null && bookIdResult.StockAvailable > 0)
                    {
                        issuedBook.IssueDate = DateTime.Today;
                        issuedBook.ExpiryDate = DateTime.Today.AddDays(30);
                        issuedBook.StaffId = issue.StaffId;
                        issuedBook.StudentId = issue.StudentId;

                        bookIdResult.StockAvailable -= 1;

                        var bookIssuedResult = await _issueRepository.AddBookIssueAsync(issuedBook, bookIdResult);
                        if (bookIssuedResult != null)
                            return bookIssuedResult;
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int? studentId = 0, string? staffId = null)
        {
            var bookIssuedToEntity = await _issueRepository.GetBookIssuedToEntityDetails(studentId, staffId);
            if (bookIssuedToEntity != null)
                return bookIssuedToEntity;
            return null;
        }

        public async Task<IEnumerable<Issue>?> GetBookIssuedAsync()
        {
            var bookIssued = await _issueRepository.GetBookIssuedAsync();
            if (bookIssued != null)
                return bookIssued;
            return null;
        }

        public async Task<Issue?> GetBookIssuedByIdAsync(short issueId)
        {
            var bookIssue = await _issueRepository.GetBookIssuedByIdAsync(issueId);
            if (bookIssue != null)
                return bookIssue;
            return null;
        }

        public async Task<Issue?> UpdateBookIssuedAsync(short issueId, Issue issue)
        {
            var bookIssuedRecord = await GetBookIssuedByIdAsync(issueId);
            if (bookIssuedRecord != null)
            {
                bookIssuedRecord.IssueId = issueId;
                bookIssuedRecord.IssueDate = issue.IssueDate;
                bookIssuedRecord.ExpiryDate = issue.ExpiryDate;
                bookIssuedRecord.BookId = issue.BookId;

                var updatedIssuedDetails = await _issueRepository.UpdateBookIssuedAsync(bookIssuedRecord);
                if (updatedIssuedDetails != null)
                    return updatedIssuedDetails;
            }
            return null;
        }

        public async Task<Issue?> DeleteIssueAsync(short issueId)
        {
            var issuedRecord = await GetBookIssuedByIdAsync(issueId);
            var deletedBookIssued = await _issueRepository.DeleteIssueAsync(issuedRecord);
            if (deletedBookIssued != null)
                return deletedBookIssued;
            return null;
        }
    }
}