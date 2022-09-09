using AutoMapper;
using LibraryManagement.Api.Configuration;
using LibraryManagement.Api.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));

#region Configure and Register AutoMapper

var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperConfiguration()));
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);

#endregion Configure and Register AutoMapper

IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemServices();
builder.Services.RegisterApplicationServices(configuration);

var app = builder.Build();
app.CreateMiddlewarePipeline();
app.Run();