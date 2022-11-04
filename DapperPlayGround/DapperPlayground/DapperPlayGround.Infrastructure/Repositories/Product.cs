using AdventureWorks.Core.Dtos;
using Dapper;
using DapperPlayGround.Infrastructure.Models;
using System.Data;

namespace DapperPlayGround.Infrastructure.Repositories
{
    public class Product : IProduct
    {
        private readonly IDbConnection _adventureContext;
        public Product(IDbConnection adventureContext)
        {
            _adventureContext = adventureContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProductDetailsByView()
        {
            var productDetailsByView = "select * from vM_ProductDetails";
            return await _adventureContext.QueryAsync<ProductDto>(productDetailsByView);
        }

        public async Task<IEnumerable<GuidData>> GetProductGuid()
        {
            var storedProcedureQuery = "execute spGetProductsGuid";
            return await _adventureContext.QueryAsync<GuidData>(storedProcedureQuery);
        }

        public async Task<IEnumerable<GuidData>> GetProductGuidByProductId(int productId)
        {
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("productId", productId, DbType.Int32);
            var guidByProductIdQuery = "spGetProductsGuidByProductId";
            return await _adventureContext.QueryAsync<GuidData>(guidByProductIdQuery, dynamicParameters, commandType: CommandType.StoredProcedure);
        }
    }
}
