// See https://aka.ms/new-console-template for more information
using AutoMapper;
using EmployeeRecordBook.Configurations;
using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Infrastructure.Data;
using EmployeeRecordBook.Infrastructure.Repositories;
using EmployeeRecordBook.Infrastructure.Repositories.Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Hello, World!");

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();

#endregion



using (var employeeContext = new EmployeeContext())
{

    IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);
    try
    {
        var employees = await employeeRepository.GetEmployeesAsync(pageIndex: 1, pageSize: 4, sortOrder: "asc", sortField: "Name");
        Console.WriteLine("\nsorted employee data:");
        foreach (var empData in employees)
        {
            Console.WriteLine($"{empData.Name}\t{empData.Email}\t{empData.Id}\t{empData.Salary}\t{empData.DepartmentName}");
        }


    }
    catch (ArgumentException a)
    {

        Console.WriteLine(a.Message);
    }
}

using (IDbConnection db = new SqlConnection(@"Server=(localDb)\MSSQLLocalDB;Database = EmployeeRecordBook;Trusted_Connection = True;"))
{
Console.WriteLine("\n\n dapper view query:");
    IEmployeeRepository employeeRepository = new EmployeeDapperRepository(db);
    var employees = await employeeRepository.GetEmployeeDetailsByView();
    foreach (var employee in employees)
    {
        Console.WriteLine($"{employee.Name} {employee.Id} {employee.Email}");
    }


Console.WriteLine("\n\n dapper stored procedure query:");
    var employeesRecordByProcedure = await employeeRepository.GetEmployeeByProcedure();
    foreach (var employee in employees)
    {
        Console.WriteLine($"{employee.Name} {employee.Id} {employee.Email}");
    }


Console.WriteLine("\n\n dapper stored procedure query with emp id:");
    var employeesRecordByProcedureById = await employeeRepository.GetEmployeeByIdProcedure();
    foreach (var employee in employeesRecordByProcedureById)
    {
        Console.WriteLine($"{employee.Name} {employee.Id} {employee.Email}");
    }
}