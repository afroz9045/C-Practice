using AdventureWorks.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using DapperPlayGround.Infrastructure.Repositories;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Collections;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {



        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            using (var dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=adventure"))
            {
                IProduct product = new Product(dbConnection);
                return await (product.GetProductDetailsByView());
            }

        }

        [HttpGet]
        [Route("{productId:Id}")]
        public async Task<IEnumerable<GuidData>> GetProductById([FromRoute] int productId)
        {
            using (var dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=adventure"))
            {
                IProduct product = new Product(dbConnection);
                return await product.GetProductGuidByProductId(productId);
            }
        }
    }
}
