using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IIssueRepository _issueRepository;

        public PenaltyRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IIssueRepository issueRepository)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _issueRepository = issueRepository;
        }

        public async Task<Penalty?> IsPenalty(short issueId)
        {
            var isPenaltyAlreadyExits = await GetPenaltyByIdAsync(issueId);
            if (isPenaltyAlreadyExits == null)
            {
                var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
                TimeSpan expiredDate = DateTime.UtcNow.Subtract(issueDetails.ExpiryDate);
                if (issueDetails != null && expiredDate.Days > 0)
                {
                    var penalty = new Penalty()
                    {
                        IssueId = issueDetails.IssueId,
                        PenaltyAmount = expiredDate.Days * 2,
                        PenaltyPaidStatus = false
                    };
                    await _libraryDbContext.Penalties.AddAsync(penalty);
                    await _libraryDbContext.SaveChangesAsync();
                    return penalty;
                }
                return null;
            }
            else if (isPenaltyAlreadyExits != null)
            {
                return isPenaltyAlreadyExits;
            }

            return null;
        }

        public async Task<IEnumerable<Penalty>?> GetPenaltiesAsync()
        {
            var penaltyQuery = "select * from [penalty]";
            var penaltyData = await _dapperConnection.QueryAsync<Penalty>(penaltyQuery);
            if (penaltyData != null)
            {
                return penaltyData;
            }
            return null;
        }

        public async Task<Penalty> GetPenaltyByIdAsync(short issueId)
        {
            var penaltyQuery = "select * from [penalty] where issueId = @issueId";
            var penaltyIssuedData = await _dapperConnection.QueryFirstOrDefaultAsync<Penalty?>(penaltyQuery, new { issueId });
            if (penaltyIssuedData == null)
            {
                return null;
            }
            else
            {
                return penaltyIssuedData;
            }
        }

        public async Task<bool> PayPenaltyAsync(short issueId, int penaltyAmount)
        {
            var penalty = await IsPenalty(issueId);
            if (penalty != null && penaltyAmount == penalty.PenaltyAmount)
            {
                //penalty.PenaltyAmount -= penaltyAmount;
                penalty.PenaltyPaidStatus = true;
                _libraryDbContext.Penalties.Update(penalty);
                await _libraryDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Penalty?> DeletePenaltyAsync(short issueId)
        {
            var penalty = await GetPenaltyByIdAsync(issueId);
            if (penalty != null)
            {
                _libraryDbContext.Penalties.Remove(penalty);
                await _libraryDbContext.SaveChangesAsync();
                return penalty;
            }
            return null;
        }
    }
}