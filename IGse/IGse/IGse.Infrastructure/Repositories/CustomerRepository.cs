using Dapper;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;
using System.Data;

namespace IGse.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(GseDbContext gseDbContext, IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customers>> GetCustomersAsync()
        {
            var customerQuery = "SELECT * FROM [IGse_DB]..[Customer] c  join Users u  on c.CustomerId = u.CustomerId  WHERE u.role <> 'Admin'";
            var customerResults = await _dbConnection.QueryAsync<Customers>(customerQuery);
            return customerResults;
        }

        public async Task<Customers> GetCustomerByIdAsync(int id)
        {
            var customerQuery = "SELECT * FROM [Customer] WHERE CustomerId = @id";
            var customerResult = await _dbConnection.QueryFirstOrDefaultAsync<Customers>(customerQuery, new { id });
            return customerResult;
        }

        public async Task<int> GetWalletAmountForCustomerId(int customerId)
        {
            var customerWalletAmountQuery = "SELECT WalletAmount from [Customer] WHERE CustomerId = @customerId";
            var walletAmount = await _dbConnection.QueryFirstOrDefaultAsync<int>(customerWalletAmountQuery, new { customerId });
            return walletAmount;
        }
        public async Task<Customers> AddCustomer(Customers customer)
        {
            _gseDbContext.Customers.Add(customer);
            await _gseDbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> UpdateCustomer(Customers customer)
        {
            _gseDbContext.Customers.Update(customer);
            await _gseDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomer(Customers Customer)
        {
            _gseDbContext.Customers.Remove(Customer);
            await _gseDbContext.SaveChangesAsync();
            return true;
        }
    }
}
