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
            _libraryDbContext.Update(book);
            _libraryDbContext.Issues.Add(issue);
            await _libraryDbContext.SaveChangesAsync();
            return issue;
        }

        public async Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int studentId, string? staffId = null)
        {
            var bookIssuedEntityStaff = await (from issued in _libraryDbContext.Issues.Include(x => x.Book)
                                               join staff in _libraryDbContext.Staffs
                                               on issued.StaffId equals staff.StaffId into st
                                               from staffRecord in st.DefaultIfEmpty()
                                               join student in _libraryDbContext.Students
                                               on issued.StudentId equals student.StudentId into stu
                                               from studentRecord in stu.DefaultIfEmpty()
                                               where (staffId == null || issued.StaffId == staffId)
                                               && (studentId <= 0 || issued.StudentId == studentId)
                                               select new BookIssuedTo
                                               {
                                                   BookId = issued.BookId,
                                                   BookName = issued.Book.BookName,
                                                   StaffId = staffRecord.StaffId,
                                                   StaffName = staffRecord.StaffName,
                                                   StudentId = studentRecord.StudentId,
                                                   StudentName = studentRecord.StudentName
                                               }).ToListAsync();
            return bookIssuedEntityStaff;
        }

        public async Task<IEnumerable<Issue>?> GetBookIssuedAsync()
        {
            var getBookIssueQuery = "select * from [issue]";
            var bookIssuedData = await _dapperConnection.QueryAsync<Issue>(getBookIssueQuery);
            return bookIssuedData;
        }

        public async Task<Issue?> GetBookIssuedByIdAsync(short issueId)
        {
            var getBookIssuedByIdQuery = "select * from [issue] where IssueId = @issueId";
            var bookIssuedData = await _dapperConnection.QueryFirstOrDefaultAsync<Issue>(getBookIssuedByIdQuery, new { issueId });
            return bookIssuedData;
        }

        public async Task<Issue?> UpdateBookIssuedAsync(Issue issue)
        {
            _libraryDbContext.Update(issue);
            await _libraryDbContext.SaveChangesAsync();
            return issue;
        }

        public async Task<Issue> DeleteIssueAsync(Issue issuedRecord)
        {
            _libraryDbContext.Issues?.Remove(issuedRecord);
            await _libraryDbContext.SaveChangesAsync();
            return issuedRecord;
        }
    }
}