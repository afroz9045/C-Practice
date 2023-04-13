using Dapper;
using JWT.Authentication.Core.Entities;
using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Server.Core.Contract.Repositories;
using System.Data;

namespace JWT.Authentication.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IGseDbContext _gseDbContext;
        private readonly IDbConnection _dbConnection;

        public UsersRepository(IGseDbContext gseDbContext,IDbConnection dbConnection)
        {
            _gseDbContext = gseDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            try
            {
                _gseDbContext.Users.Add(user);
                await _gseDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            var userQuery = "SELECT * FROM [Users] WHERE EmailId = @email";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<Users>(userQuery, new { email });
            return user;
        }

        public async Task<int> GetCustomerIdByUserId(int userId)
        {
            var customerIdQuery = "Exec GetCustomerIdByUserID @UserId = @userId";
            var customerId = await _dbConnection.QueryFirstOrDefaultAsync<int>(customerIdQuery, new {userId});
            return customerId;
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            var userQuery = "SELECT * FROM [Users] WHERE UserId = @id";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<Users>(userQuery, new { id });
            return user;
        }
        
        public async Task<bool> DeleteUserAsync(Users user)
        {
            _gseDbContext.Users.Remove(user);
            await _gseDbContext.SaveChangesAsync();
            return true;
        }
    }
}
