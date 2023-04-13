using IGse.Core.Dtos;
using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface ICustomerEvcHistoryRepository
    {
        Task<CustomerEvcHistory> AddCustomerEvcHistory(CustomerEvcHistory customerEvcHistory);
        Task<IEnumerable<CustomerEvcHistory>> GetCustomerEvcHistory();
        Task<IEnumerable<CustomerEvcHistoryDto>> GetCustomerEvcHistoryByCustomerId(int customerId);
    }
}