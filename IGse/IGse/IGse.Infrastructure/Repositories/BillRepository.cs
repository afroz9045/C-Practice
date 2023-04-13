using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public BillRepository(GseDbContext gseDbContext, IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Bill>> GetBillsAsync()
        {
            var billsQuery = "SELECT * FROM [IGse_DB].[dbo].[Bill]";
            var billsResult = await _dbConnection.QueryAsync<Bill>(billsQuery);
            return billsResult;
        }

        public async Task<Bill> GetBillByBillIdAsync(int id)
        {
            var billQuery = "SELECT * FROM [Bill] WHERE BillId = @id";
            var resultantBill = await _dbConnection.QueryFirstOrDefaultAsync<Bill>(billQuery, new { id });
            return resultantBill;
        }

        public async Task<Bill> AddBillAsync(Bill bill)
        {
            _gseDbContext.Bills.Add(bill);
            await _gseDbContext.SaveChangesAsync();
            return bill;
        }

        public async Task<IEnumerable<Bill>> GetBillsForCustomerId(int customerId)
        {
            var billQuery = "SELECT * FROM [Bill] WHERE CustomerId = @customerId";
            var resultantBill = await _dbConnection.QueryAsync<Bill>(billQuery, new { customerId });
            return resultantBill.ToList();
        }
    }
}
