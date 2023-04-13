using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface ISetPriceRepository
    {
        Task<SetPrice> SetPrice(SetPrice setPrice);
        Task<SetPrice> GetPriceData();
        Task<SetPrice> UpdatePrice(SetPrice setPrice);

    }
}