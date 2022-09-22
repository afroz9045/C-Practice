using LibraryManagement.Core.Constants;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Services;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolManagementAPI.Infrastructure.Configuration;
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
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }
                );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            services.AddSwaggerGen();
            services.ConfigureOptions<ConfigureSwaggerOptions>();
            services.AddDataProtection();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });
        }

        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryManagementSystemDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("LibraryManagementDbContext")));
            services.Configure<Constants>(configuration.GetSection("Constants"));

            // Resolving dependencies for services
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IPenaltyService, PenaltyService>();
            services.AddScoped<IReturnService, ReturnService>();

            // Resolving dependencies for repositories
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IPenaltyRepository, PenaltyRepository>();
            services.AddScoped<IReturnRepository, ReturnRepository>();

            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("LibraryManagementDbContext")));
        }
    }
}