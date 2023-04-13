using IGse.Core.Entities;

namespace IGse.Core.Contracts.Services
{
    public interface IBillService
    {
        Task<Bill?> GetBillAsync(Bill bill);
        Task<IEnumerable<Bill>> GetBillsAsync();
    }
}