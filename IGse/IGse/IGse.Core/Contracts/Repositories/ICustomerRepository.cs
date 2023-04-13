using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customers>> GetCustomersAsync();
        Task<Customers> GetCustomerByIdAsync(int id);
        Task<int> GetWalletAmountForCustomerId(int customerId);
        Task<Customers> AddCustomer(Customers customer);
        Task<bool> UpdateCustomer(Customers customer);
        Task<bool> DeleteCustomer(Customers Customer);
    }
}