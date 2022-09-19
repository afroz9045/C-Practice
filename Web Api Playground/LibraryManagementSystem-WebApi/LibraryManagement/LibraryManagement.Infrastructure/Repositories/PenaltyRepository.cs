using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class PenaltyRepository : IPenaltyRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IPenaltyService _penaltyService;
        private readonly IDbConnection _dapperConnection;
        private readonly IIssueRepository _issueRepository;

        public PenaltyRepository(LibraryManagementSystemDbContext libraryDbContext, IPenaltyService penaltyService, IDbConnection dapperConnection, IIssueRepository issueRepository)
        {
            _libraryDbContext = libraryDbContext;
            _penaltyService = penaltyService;
            _dapperConnection = dapperConnection;
            _issueRepository = issueRepository;
        }

        public async Task<Penalty?> IsPenalty(short issueId, Penalty? existingPenalty, Issue? bookIssueDetails)
        {
            var isPenalty = _penaltyService.IsPenalty(issueId, existingPenalty, bookIssueDetails);
            if (isPenalty != null && existingPenalty == null)
            {
                await _libraryDbContext.Penalties.AddAsync(isPenalty);
                await _libraryDbContext.SaveChangesAsync();
                return isPenalty;
            }
            else if (isPenalty != null && existingPenalty != null)
            {
                return existingPenalty;
            }
            return null;
        }

        public async Task<IEnumerable<Penalty>?> GetPenaltiesAsync()
        {
            var penaltyQuery = "select * from [penalty]";
            var penaltyData = await _dapperConnection.QueryAsync<Penalty>(penaltyQuery);
            return penaltyData;
        }

        public async Task<Penalty?> GetPenaltyByIdAsync(short? issueId)
        {
            var penaltyQuery = "select * from [penalty] where issueId = @issueId";
            var penaltyIssuedData = await _dapperConnection.QueryFirstOrDefaultAsync<Penalty?>(penaltyQuery, new { issueId });
            return penaltyIssuedData;
        }

        public async Task<Penalty?> PayPenaltyAsync(Penalty penalty)
        {
            _libraryDbContext.Penalties.Update(penalty);
            await _libraryDbContext.SaveChangesAsync();
            return penalty;
        }

        public async Task<Penalty?> DeletePenaltyAsync(Penalty penalty)
        {
            _libraryDbContext.Penalties.Remove(penalty);
            await _libraryDbContext.SaveChangesAsync();
            return penalty;
        }
    }
}