using IGse.Core.Dtos;
using IGse.Core.Entities;

namespace IGse.Core.Contracts.Services
{
    public interface ICustomerService
    {
        Task<Customers> AddCustomerAsync(Customers customer);
        Task<bool> AddOrUpdateWalletAmount(Customers customer, int amount);
    }
}