using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
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

        public async Task<Penalty?> IsPenalty(Penalty penalty)
        {
            await _libraryDbContext.Penalties.AddAsync(penalty);
            await _libraryDbContext.SaveChangesAsync();
            return penalty;
        }

        public async Task<IEnumerable<Penalty>?> GetPenaltiesAsync()
        {
            var penaltyQuery = "select * from [penalty]";
            var penaltyData = await _dapperConnection.QueryAsync<Penalty>(penaltyQuery);
            return penaltyData;
        }

        public async Task<Penalty?> GetPenaltyByIdAsync(short issueId)
        {
            var penaltyQuery = "select * from [penalty] where issueId = @issueId";
            var penaltyIssuedData = await _dapperConnection.QueryFirstOrDefaultAsync<Penalty?>(penaltyQuery, new { issueId });
            return penaltyIssuedData;
        }

        public async Task<bool> PayPenaltyAsync(Penalty penalty)
        {
            if (penalty != null && penalty.PenaltyPaidStatus == true)
            {
                _libraryDbContext.Penalties.Update(penalty);
                await _libraryDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Penalty?> DeletePenaltyAsync(Penalty penalty)
        {
            _libraryDbContext.Penalties.Remove(penalty);
            await _libraryDbContext.SaveChangesAsync();
            return penalty;
        }
    }
}