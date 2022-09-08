using Dapper;
using LibraryManagement.Core.Contracts;
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

        public async Task<Return?> AddReturnAsync(Return returnDetails, short issueId)
        {
            var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
            var bookDetails = await _bookRepository.GetBookById(issueDetails.BookId);
            if (issueDetails == null)
            {
                return null;
            }
            var isPenalty = await _penaltyRepository.IsPenalty(issueId);
            var penaltyData = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            if (penaltyData.PenaltyAmount == 0 && penaltyData.PenaltyPaidStatus == true || penaltyData == null)
            {
                var returnRecord = new Return()
                {
                    ExpiryDate = issueDetails.ExpiryDate,
                    IssueDate = issueDetails.IssueDate,
                    BookId = issueDetails.BookId,
                    ReturnDate = DateTime.UtcNow
                };
                await _issueRepository.DeleteIssueAsync(issueId);
                _libraryDbContext.Returns.Add(returnRecord);
                bookDetails.StockAvailable += 1;
                _libraryDbContext.Books.Update(bookDetails);
                await _libraryDbContext.SaveChangesAsync();
                return returnRecord;
            }
            return null;
        }

        public async Task<IEnumerable<Return>> GetReturnAsync()
        {
            var getReturnQuery = "select * from [return]";
            var returnData = await _dapperConnection.QueryAsync<Return>(getReturnQuery);
            return returnData;
        }

        public async Task<Return> GetReturnByIdAsync(int returnId)
        {
            var getReturnByIdQuery = "select * from [return] where returnId = @returnId";
            return (await _dapperConnection.QueryFirstAsync<Return>(getReturnByIdQuery, new { returnId = returnId }));
        }

        public async Task<Return> UpdateReturnAsync(int returnId, Return returnDetails)
        {
            var returnRecord = await GetReturnByIdAsync(returnId);

            returnRecord.ReturnId = returnId;
            returnRecord.ExpiryDate = returnDetails.ExpiryDate;
            returnRecord.IssueDate = returnDetails.IssueDate;
            returnRecord.BookId = returnDetails.BookId;
            returnRecord.ReturnDate = DateTime.UtcNow;

            _libraryDbContext.Update(returnRecord);
            await _libraryDbContext.SaveChangesAsync();
            return returnRecord;
        }

        public async Task<Return> DeleteReturnAsync(int returnId)
        {
            var returnRecord = await GetReturnByIdAsync(returnId); ;
            _libraryDbContext.Returns?.Remove(returnRecord);
            await _libraryDbContext.SaveChangesAsync();
            return returnRecord;
        }
    }
}