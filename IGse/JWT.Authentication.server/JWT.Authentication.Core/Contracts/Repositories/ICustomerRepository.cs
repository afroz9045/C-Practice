using JWT.Authentication.Core.Entities;

namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customers> AddCustomer(Customers customer);
        Task<bool> DeleteCustomer(Customers Customer);
        Task<Customers> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Customers>> GetCustomersAsync();
        Task<bool> UpdateCustomer(Customers customer);
    }
}