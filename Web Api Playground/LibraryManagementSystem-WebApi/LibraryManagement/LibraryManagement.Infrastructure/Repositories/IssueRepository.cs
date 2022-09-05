using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;
using System.Net;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IBookRepository _bookRepository;

        public IssueRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IBookRepository bookRepository)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _bookRepository = bookRepository;
        }

        public async Task<Issue> AddBookIssueAsync(Issue issue)
        {
            var issuedBook = new Issue();

            issuedBook.BookId = issue.BookId;
            var bookIdResult = await _bookRepository.GetBookById(issuedBook.BookId);

            if (bookIdResult != null && bookIdResult.StockAvailable > 0)
            {
                issuedBook.IssueDate = DateTime.Today;
                issuedBook.ExpiryDate = DateTime.Today.AddDays(30);
                bookIdResult.StockAvailable -= 1;

                _libraryDbContext.Update(bookIdResult);
                _libraryDbContext.Issues.Add(issuedBook);
                await _libraryDbContext.SaveChangesAsync();
            }
            return issuedBook;
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