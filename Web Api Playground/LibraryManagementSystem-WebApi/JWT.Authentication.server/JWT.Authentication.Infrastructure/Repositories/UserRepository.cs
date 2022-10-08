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
            return await _dbContext.UserDetails.AnyAsync(s => s.Email == email && s.Password == password);
        }

        public async Task<bool> RegisterUser(UserDetail user)
        {
            await _dbContext.UserDetails.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<UserDetail> GetUserDetails(string email)
        {
            return await _dbContext.UserDetails.FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}