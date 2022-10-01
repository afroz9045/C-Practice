using JWT.Authentication.Core.Entities;
using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Server.Core.Contract.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Server.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryManagementSystemDbContext _dbContext;

        public UserRepository(LibraryManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            return await _dbContext.Credentials.AnyAsync(s => s.Email == email && s.Password == password);
        }

        public async Task<bool> RegisterUser(Credential user)
        {
            await _dbContext.Credentials.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Credential> GetUserDetails(string email)
        {
            return await _dbContext.Credentials.FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}