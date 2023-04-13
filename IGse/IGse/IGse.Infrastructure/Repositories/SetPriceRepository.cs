using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class SetPriceRepository : ISetPriceRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public SetPriceRepository(GseDbContext gseDbContext,IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<SetPrice> SetPrice(SetPrice setPrice)
        {
            _gseDbContext.SetPrices.Add(setPrice);
            await _gseDbContext.SaveChangesAsync();
            return setPrice;
        }

        public async Task<SetPrice> GetPriceData()
        {
            var getPriceQuery = "SELECT * FROM [SetPrice]";
            var priceData = await _dbConnection.QueryFirstOrDefaultAsync<SetPrice>(getPriceQuery);
            return priceData;
        }

        public async Task<SetPrice> UpdatePrice(SetPrice setPrice)
        {
            _gseDbContext.SetPrices.Update(setPrice);
            await _gseDbContext.SaveChangesAsync();
            return setPrice;
        }
    }
}
