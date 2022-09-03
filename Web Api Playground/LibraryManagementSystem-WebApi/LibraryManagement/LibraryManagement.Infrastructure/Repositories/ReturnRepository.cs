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

        public ReturnRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IIssueRepository issueRepository)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _issueRepository = issueRepository;
        }

        public async Task<Return?> AddReturnAsync(Return returnDetails, short issueId)
        {
            var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
            if (issueDetails == null)
            {
                return null;
            }
            var returnRecord = new Return()
            {
                ReturnId = returnDetails.ReturnId,
                ExpiryDate = issueDetails.ExpiryDate,
                IssueDate = issueDetails.IssueDate,
                BookId = issueDetails.BookId
            };
            await _issueRepository.DeleteDepartmentAsync(issueId);
            _libraryDbContext.Returns.Add(returnRecord);
            await _libraryDbContext.SaveChangesAsync();
            return returnRecord;
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