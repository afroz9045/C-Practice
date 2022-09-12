using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Services;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json.Serialization;

namespace LibraryManagement.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterSystemServices(this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers().AddJsonOptions(options =>
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryManagementSystemDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("LibraryManagementDbContext")));

            // Resolving dependencies for services
            services.AddScoped<IBookService, BookService>();

            // Resolving dependencies for repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IIssueRepository, IssueRepository>();
            services.AddTransient<IPenaltyRepository, PenaltyRepository>();
            services.AddTransient<IReturnRepository, ReturnRepository>();

            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("LibraryManagementDbContext")));
        }
    }
}