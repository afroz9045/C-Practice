using JWT.Authentication.Infrastructure.DataContext;
using JWT.Authentication.Infrastructure.Repositories;
using JWT.Authentication.Server.Core.Contract.Repositories;
using JWT.Authentication.Server.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JWT.Authentication.Server.Infrastructure.Extensions
{
    public static class ServiceCollectionExtenions
    {
        public static void AddConfigurationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Register Repository

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IStaffRepository, StaffRepository>();

            #endregion Register Repository

            #region Database

            services.AddScoped<LibraryManagementSystemDbContext>()
                     .AddDbContextPool<LibraryManagementSystemDbContext>(options =>
                     {
                         options.UseSqlServer(configuration.GetConnectionString("IdentityDbContext"));
                     });

            #endregion Database
        }
    }
}