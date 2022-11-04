using AdventureWorks.Core.Dtos;

namespace DapperPlayGround.Infrastructure.Repositories
{
    public interface IProduct
    {
        Task<IEnumerable<ProductDto>> GetProductDetailsByView();
        Task<IEnumerable<GuidData>> GetProductGuid();
        Task<IEnumerable<GuidData>> GetProductGuidByProductId(int productId);
    }
}