using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Infrastructure.Repositories;
using JWT.Authentication.Server.Core.Contract.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace JWT.Authentication.Server.Infrastructure.Extensions
{
    public static class ServiceCollectionExtenions
    {
        public static void AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Register Repository
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            #endregion Register Repository

            #region Database

            services.AddScoped<IGseDbContext>()
                     .AddDbContextPool<IGseDbContext>(options =>
                     {
                         options.UseSqlServer(configuration.GetConnectionString("IGseDbContext"));
                     });
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("IGseDbContext")));

            #endregion Database
        }
    }
}