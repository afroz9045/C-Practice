using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface ISetPriceHistoryRepository
    {
        Task<SetPriceHistory> SetPriceHistoryAsync(SetPriceHistory setPriceHistory);
        Task<IEnumerable<SetPriceHistory>> GetSetPriceHistory();
    }
}