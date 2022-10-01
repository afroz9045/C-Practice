using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IIssueRepository _issueRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IPenaltyRepository _penaltyRepository;

        public ReturnRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IIssueRepository issueRepository, IBookRepository bookRepository, IPenaltyRepository penaltyRepository)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _issueRepository = issueRepository;
            _bookRepository = bookRepository;
            _penaltyRepository = penaltyRepository;
        }

        public async Task<Return?> AddReturnAsync(Return returnDetails, Book book, Issue bookIssue)
        {
            _libraryDbContext.Returns.Add(returnDetails);
            _libraryDbContext.Books.Update(book);
            await _libraryDbContext.SaveChangesAsync();
            return returnDetails;
        }

        public async Task<IEnumerable<Return?>> GetReturnAsync()
        {
            var getReturnQuery = "select * from [return]";
            var returnData = await _dapperConnection.QueryAsync<Return>(getReturnQuery);
            return returnData;
        }

        public async Task<Return?> GetReturnByIdAsync(int returnId)
        {
            var getReturnByIdQuery = "select * from [return] where returnId = @returnId";
            return (await _dapperConnection.QueryFirstOrDefaultAsync<Return>(getReturnByIdQuery, new { returnId }));
        }

        public async Task<IEnumerable<Return>> GetBooksReturnedByDateRange(DateTime fromDate, DateTime? toDate = null)
        {
            if (toDate == null)
                toDate = DateTime.UtcNow;
            var booksReturnedOnDateRangeQuery = "exec SpGetBooksReturnedByDateRange @fromDate,@toDate";
            var resultantBooksReturned = await _dapperConnection.QueryAsync<Return>(booksReturnedOnDateRangeQuery, new { fromDate, toDate });
            return resultantBooksReturned;
        }

        public async Task<Return> UpdateReturnAsync(Return returnDetails)
        {
            _libraryDbContext.Update(returnDetails);
            await _libraryDbContext.SaveChangesAsync();
            return returnDetails;
        }

        public async Task<Return> DeleteReturnAsync(Return bookReturn)
        {
            _libraryDbContext.Returns?.Remove(bookReturn);
            await _libraryDbContext.SaveChangesAsync();
            return bookReturn;
        }
    }
}