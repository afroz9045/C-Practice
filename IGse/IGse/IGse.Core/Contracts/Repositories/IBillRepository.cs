using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface IBillRepository
    {
        Task<Bill> AddBillAsync(Bill bill);
        Task<Bill> GetBillByBillIdAsync(int id);
        Task<IEnumerable<Bill>> GetBillsForCustomerId(int customerId);
        Task<IEnumerable<Bill>> GetBillsAsync();
    }
}