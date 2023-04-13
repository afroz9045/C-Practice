using Dapper;
using JWT.Authentication.Core.Entities;
using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Server.Core.Contract.Repositories;
using System.Data;

namespace JWT.Authentication.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IGseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IGseDbContext gseDbContext, IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customers>> GetCustomersAsync()
        {
            var customerQuery = "SELECT * from [Customer]";
            var customerResults = await _dbConnection.QueryAsync<Customers>(customerQuery);
            return customerResults;
        }

        public async Task<Customers> GetCustomerByIdAsync(int id)
        {
            var customerQuery = "SELECT * FROM [Customer] WHERE CustomerId = @id";
            var customerResult = await _dbConnection.QueryFirstOrDefaultAsync<Customers>(customerQuery, new { id });
            return customerResult;
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
