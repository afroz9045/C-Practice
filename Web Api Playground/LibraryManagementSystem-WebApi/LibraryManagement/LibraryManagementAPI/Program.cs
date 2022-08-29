using LibraryManagement.Core.Contracts;
using LibraryManagement.Infrastructure.Data;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LibraryManagementSystemDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryManagementDbContext")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddTransient<IDbConnection>(db => new SqlConnection(
                    builder.Configuration.GetConnectionString("LibraryManagementDbContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();