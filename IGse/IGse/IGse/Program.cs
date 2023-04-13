using AutoMapper;
using IGse.Api.Extensions;
using IGse.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
IConfiguration configuration = builder.Configuration;
builder.Services.RegisterSystemServices();
builder.Services.RegisterApplicationServices(configuration);

#region Configure and Register AutoMapper

var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperConfiguration()));

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);

#endregion Configure and Register AutoMapper

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
//var app = builder.Build();
app.CreateMiddlewarePipeline();
app.Run();
