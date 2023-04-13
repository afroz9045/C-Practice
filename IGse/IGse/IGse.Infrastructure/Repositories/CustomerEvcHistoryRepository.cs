using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Dtos;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class CustomerEvcHistoryRepository : ICustomerEvcHistoryRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public CustomerEvcHistoryRepository(GseDbContext gseDbContext, IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }


        public async Task<CustomerEvcHistory> AddCustomerEvcHistory(CustomerEvcHistory customerEvcHistory)
        {
            _gseDbContext.CustomerEvcHistory.Add(customerEvcHistory);
            await _gseDbContext.SaveChangesAsync();
            return customerEvcHistory;
        }

        public async Task<IEnumerable<CustomerEvcHistoryDto>> GetCustomerEvcHistoryByCustomerId(int customerId)
        {
            var evcHistoryQuery = "EXEC GetEvcHistoryByCustomerId @customerId";
            var evcHistory = await _dbConnection.QueryAsync<CustomerEvcHistoryDto>(evcHistoryQuery, new { customerId });
            return evcHistory;
        }

        public async Task<IEnumerable<CustomerEvcHistory>> GetCustomerEvcHistory()
        {
            var evcHistoryQuery = "SELECT * FROM [CustomerEvcHistory]";
            var evcHistory = await _dbConnection.QueryAsync<CustomerEvcHistory>(evcHistoryQuery);
            return evcHistory;
        }
    }
}
