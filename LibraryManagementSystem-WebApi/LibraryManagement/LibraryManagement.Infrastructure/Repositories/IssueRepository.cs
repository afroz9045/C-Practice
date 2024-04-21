using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public IssueRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<Issue?> AddBookIssueAsync(Issue? issue, Book? book)
        {
            if (issue != null && book != null)
            {
                _libraryDbContext.Update(book);
                var addedBookIssued = await _libraryDbContext.Issues.AddAsync(issue);
                await _libraryDbContext.SaveChangesAsync();
                return issue;
            }
            return null;
        }

        public int GetBooksToBeReturnByEntity(int? studentId, string? staffId)
        {
            var booksToBeReturnQuery = "exec SpGetCountOfBooksReturnByEntity @studentId,@staffId";
            var resultBooksToBeReturn = _dapperConnection.QuerySingleOrDefault<int>(booksToBeReturnQuery, new { studentId, staffId });
            return resultBooksToBeReturn;
        }

        public async Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int studentId, string? staffId = null)
        {
            var spToGetNumberOfBooksIssuedToEntity = "exec SpGetBookIssuedToEntity @StudentId ,@staffId";
            var resultantBooksIssued = await _dapperConnection.QueryAsync<BookIssuedTo>(spToGetNumberOfBooksIssuedToEntity, new { studentId, staffId });
            return resultantBooksIssued;
        }

        public async Task<IEnumerable<Issue>?> GetBookIssuedByBookId(int bookId)
        {
            var bookIssuedRecordsByBookIdQuery = "select * from issue where BookId = @bookId";
            var bookIssuedData = await _dapperConnection.QueryAsync<Issue>(bookIssuedRecordsByBookIdQuery, new { bookId });
            return bookIssuedData;
        }

        public async Task<IEnumerable<Issue>> GetBooksIssuedByDateRange(DateTime fromDate, DateTime? toDate = null)
        {
            if (toDate == null)
                toDate = DateTime.UtcNow;
            var booksIssuedOnDateRangeQuery = "exec SpGetBooksIssuedByDateRange @fromDate,@toDate";
            var resultantBooksIssued = await _dapperConnection.QueryAsync<Issue>(booksIssuedOnDateRangeQuery, new { fromDate, toDate });
            return resultantBooksIssued;
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

        public async Task<Issue?> UpdateBookIssuedAsync(Issue? issue)
        {
            if (issue != null)
            {
                _libraryDbContext.Update(issue);
                await _libraryDbContext.SaveChangesAsync();
                return issue;
            }
            return null;
        }

        public async Task<Issue?> DeleteIssueAsync(Issue issuedRecord)
        {
            _libraryDbContext.Issues?.Remove(issuedRecord);
            await _libraryDbContext.SaveChangesAsync();
            return issuedRecord;
        }
    }
}