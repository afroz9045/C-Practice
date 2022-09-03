using LibraryManagement.Core.Contracts;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagementAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterSystemServices(this IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryManagementSystemDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("LibraryManagementDbContext")));

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddTransient<IIssueRepository, IssueRepository>();
            services.AddTransient<IReturnRepository, ReturnRepository>();
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("LibraryManagementDbContext")));
        }
    }
}