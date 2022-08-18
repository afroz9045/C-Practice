// See https://aka.ms/new-console-template for more information
using DapperPlayGround.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

using (var dbConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=adventure"))
{

    Product product = new Product(dbConnection);

    var productByView = await product.GetProductDetailsByView();
    Console.WriteLine("\n\nProduct data by view:\n");
    foreach (var data in productByView)
    {
        Console.WriteLine($"{data.Name}\t\t {data.ProductNumber}\t\t{data.ProductId}");
    }

    var productGuidByStoredProcedure = await product.GetProductGuid();
    Console.WriteLine("\n\nProduct Guid by Stored Procedure:\n");
    foreach (var data in productGuidByStoredProcedure)
    {
        Console.WriteLine($"{data.ProductId} {data.SafetyStockLevel} {data.ReorderPoint} {data.RowGuid}");
    }

    var productGuidByProductId = await product.GetProductGuidByProductId(316);
    Console.WriteLine("\n\n Product Guid by Product Id");
    foreach (var data in productGuidByProductId)
    {
        Console.WriteLine($"{data.ProductId} {data.SafetyStockLevel} {data.ReorderPoint} {data.RowGuid}");
    }
}