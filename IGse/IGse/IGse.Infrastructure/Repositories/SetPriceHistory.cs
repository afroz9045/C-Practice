using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class SetPriceHistoryRepository : ISetPriceHistoryRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public SetPriceHistoryRepository(GseDbContext gseDbContext,IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<SetPriceHistory> SetPriceHistoryAsync(SetPriceHistory setPriceHistory)
        {
            _gseDbContext.SetPriceHistory.Add(setPriceHistory);
            await _gseDbContext.SaveChangesAsync();
            return setPriceHistory;
        }

        public async Task<IEnumerable<SetPriceHistory>> GetSetPriceHistory()
        {
            var setPriceHistoryQuery = "SELECT * FROM [SetPriceHistory]";
            var setPriceHistory = await _dbConnection.QueryAsync<SetPriceHistory>(setPriceHistoryQuery);
            return setPriceHistory;
        }
    }
}
