using IGse.Api.Configuration;
using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Services;
using IGse.Infrastructure.Data;
using IGse.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace IGse.Api.Extensions
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

            #region Swagger

            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Jwt Authentication"
                });
                option.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            #endregion Swagger
        }

        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GseDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("IGseDbContext")));

            // Resolving dependencies for services
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IEvcService, EvcService>();
            services.AddTransient<ISetPriceService, SetPriceService>();
            services.AddTransient<IBillService, BillService>();
            services.AddTransient<IPaymentService, PaymentService>();

            // Resolving dependencies for repositories
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEvcRepository, EvcRepository>();
            services.AddTransient<ISetPriceRepository, SetPriceRepository>();
            services.AddTransient<ISetPriceHistoryRepository, SetPriceHistoryRepository>();
            services.AddTransient<IBillRepository, BillRepository>();
            services.AddTransient<IAdminRepository, AdminRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<ICustomerEvcHistoryRepository, CustomerEvcHistoryRepository>();

            services.AddTransient<IDbConnection>(db => new SqlConnection(
                                configuration.GetConnectionString("IGseDbContext")));

            #region Authentication

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.FromHours(1)
                    };
                });

            #endregion Authentication
        }
    }
}