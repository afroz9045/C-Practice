using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IBookRepository _bookRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IStudentRepository _studentRepository;

        public IssueRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IBookRepository bookRepository, IStaffRepository staffRepository, IStudentRepository studentRepository)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _bookRepository = bookRepository;
            _staffRepository = staffRepository;
            _studentRepository = studentRepository;
        }

        public async Task<Issue> AddBookIssueAsync(Issue issue)
        {
            var issuedBook = new Issue();
            if (issue.StaffId != null || issue.StudentId != null)
            {
                issuedBook.BookId = issue.BookId;
                var bookIdResult = await _bookRepository.GetBookById(issuedBook.BookId);
                var staffIdValidate = await _staffRepository.GetStaffByIDAsync(issue.StaffId);
                var studentIdValidate = await _studentRepository.GetStudentByIdAsync(issue.StudentId ?? 0);
                if (staffIdValidate != null || studentIdValidate != null)
                {
                    if (bookIdResult != null && bookIdResult.StockAvailable > 0)
                    {
                        //issuedBook.BookId = bookIdResult.BookId;
                        issuedBook.IssueDate = DateTime.Today;
                        issuedBook.ExpiryDate = DateTime.Today.AddDays(30);
                        issuedBook.StaffId = issue.StaffId;
                        issuedBook.StudentId = issue.StudentId;

                        bookIdResult.StockAvailable -= 1;

                        _libraryDbContext.Update(bookIdResult);
                        _libraryDbContext.Issues.Add(issuedBook);
                        await _libraryDbContext.SaveChangesAsync();
                    }
                }
            }
            return issuedBook;
        }

        public async Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int? studentId = 0, string? staffId = null)
        {
            if (staffId != null && studentId == 0)
            {
                var bookIssuedEntityStaff = await (from issuedStaff in _libraryDbContext.Issues
                                                   join staff in _libraryDbContext.Staffs on issuedStaff.StaffId equals staffId
                                                   where staff.StaffId == staffId
                                                   select new BookIssuedTo
                                                   {
                                                       BookId = issuedStaff.BookId,
                                                       StaffId = staff.StaffId,
                                                       StaffName = staff.StaffName
                                                   }).ToListAsync();
                return bookIssuedEntityStaff;
            }
            else if (staffId == null && studentId != 0)
            {
                var bookIssuedEntityStudent = await (from issuedStudent in _libraryDbContext.Issues
                                                     join student in _libraryDbContext.Students on issuedStudent.StudentId equals student.StudentId
                                                     where student.StudentId == studentId
                                                     select new BookIssuedTo
                                                     {
                                                         BookId = issuedStudent.BookId,
                                                         StudentId = student.StudentId,
                                                         StudentName = student.StudentName
                                                     }).ToListAsync();
                return bookIssuedEntityStudent;
            }
            return null;
        }

        public async Task<IEnumerable<Issue>> GetBookIssuedAsync()
        {
            var getBookIssueQuery = "select * from [issue]";
            var bookIssuedData = await _dapperConnection.QueryAsync<Issue>(getBookIssueQuery);
            return bookIssuedData;
        }

        public async Task<Issue> GetBookIssuedByIdAsync(short issueId)
        {
            var getBookIssuedByIdQuery = "select * from [issue] where issueId = @issueId";
            var bookIssuedData = (await _dapperConnection.QueryFirstAsync<Issue>(getBookIssuedByIdQuery, new { issueId = issueId }));
            return bookIssuedData;
        }

        public async Task<Issue> UpdateBookIssuedAsync(short issueId, Issue issue)
        {
            var bookIssuedRecord = await GetBookIssuedByIdAsync(issueId);

            bookIssuedRecord.IssueId = issueId;
            bookIssuedRecord.IssueDate = issue.IssueDate;
            bookIssuedRecord.ExpiryDate = issue.ExpiryDate;
            bookIssuedRecord.BookId = issue.BookId;

            _libraryDbContext.Update(bookIssuedRecord);
            await _libraryDbContext.SaveChangesAsync();
            return bookIssuedRecord;
        }

        public async Task<Issue> DeleteIssueAsync(short IssueId)
        {
            var issueRecord = await GetBookIssuedByIdAsync(IssueId);
            _libraryDbContext.Issues?.Remove(issueRecord);
            await _libraryDbContext.SaveChangesAsync();
            return issueRecord;
        }
    }
}