using JWT.Authentication.Core.Entities;

namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface IUsersRepository
    {
        Task<Users> AddUserAsync(Users user);
        Task<Users> GetUserByEmailAsync(string email);
        Task<Users> GetUserByIdAsync(int id);
        Task<int> GetCustomerIdByUserId(int userId);
        Task<bool> DeleteUserAsync(Users user);
    }
}