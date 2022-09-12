using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
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

        public async Task<Issue?> AddBookIssueAsync(Issue issue, Book book)
        {
            if (issue != null && book != null)
            {
                _libraryDbContext.Update(book);
                _libraryDbContext.Issues.Add(issue);
                await _libraryDbContext.SaveChangesAsync();
                return issue;
            }
            return null;
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

        public async Task<IEnumerable<Issue>?> GetBookIssuedAsync()
        {
            var getBookIssueQuery = "select * from [issue]";
            var bookIssuedData = await _dapperConnection.QueryAsync<Issue>(getBookIssueQuery);
            if (bookIssuedData != null)
                return bookIssuedData;
            return null;
        }

        public async Task<Issue?> GetBookIssuedByIdAsync(short issueId)
        {
            var getBookIssuedByIdQuery = "select * from [issue] where IssueId = @issueId";
            var bookIssuedData = await _dapperConnection.QueryFirstOrDefaultAsync<Issue>(getBookIssuedByIdQuery, new { issueId });
            if (bookIssuedData != null)
                return bookIssuedData;
            return null;
        }

        public async Task<Issue?> UpdateBookIssuedAsync(Issue issue)
        {
            if (issue != null)
            {
                _libraryDbContext.Update(issue);
                await _libraryDbContext.SaveChangesAsync();
                return issue;
            }
            return null;
        }

        public async Task<Issue> DeleteIssueAsync(Issue issuedRecord)
        {
            _libraryDbContext.Issues?.Remove(issuedRecord);
            await _libraryDbContext.SaveChangesAsync();
            return issuedRecord;
        }
    }
}