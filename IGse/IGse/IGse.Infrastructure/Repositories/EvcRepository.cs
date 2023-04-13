using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class EvcRepository : IEvcRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public EvcRepository(GseDbContext gseDbContext, IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Evc>> GetEvcsAsync()
        {
            var evcQuery = "SELECT * FROM [Evc]";
            var evcResult = await _dbConnection.QueryAsync<Evc>(evcQuery);
            return evcResult;
        }

        public async Task<Evc> GetEvcByIdAsync(int evcId)
        {
            var evcQuery = "SELECT * FROM [Evc] WHERE EvcId = @evcId";
            var evcResult = await _dbConnection.QueryFirstOrDefaultAsync<Evc>(evcQuery, new { evcId });
            return evcResult;
        }

        public async Task<Evc> AddEvcAsync(Evc evc)
        {
            _gseDbContext.Evcs.Add(evc);
            await _gseDbContext.SaveChangesAsync();
            return evc;
        }

        public async Task<Evc?> GetSubsidyEvc()
        {
            var subsidyEvcQuery = await _gseDbContext.Evcs.FirstOrDefaultAsync(x => x.Amount == 200 && !x.IsUsed);
            return subsidyEvcQuery;
        }

        public async Task<Evc> UpdateEvcAsync(Evc evc)
        {
            _gseDbContext.Evcs.Update(evc);
            await _gseDbContext.SaveChangesAsync();
            return evc;
        }

        public async Task<bool> DeleteEvcAsync(Evc evc)
        {
            _gseDbContext.Evcs.Remove(evc);
            await _gseDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Evc> GetEvcByVoucher(string voucherCode)
        {
            var getEvcByVoucherQuery = "SELECT * FROM [EVC] WHERE EvcVoucher = @voucherCode";
            var evc = await _dbConnection.QueryFirstOrDefaultAsync<Evc>(getEvcByVoucherQuery, new { voucherCode});
            return evc;
        }
    }
}
