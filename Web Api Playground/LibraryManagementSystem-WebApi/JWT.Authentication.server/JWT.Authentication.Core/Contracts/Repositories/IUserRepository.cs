using JWT.Authentication.Core.Entities;

namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface IUserRepository
    {
        public Task<bool> ValidateUser(string email, string password);

        public Task<bool> RegisterUser(UserDetail user);

        Task<UserDetail> GetUserDetails(string email);
    }
}