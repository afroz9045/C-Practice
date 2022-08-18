// See https://aka.ms/new-console-template for more information
using AutoMapper;
using EmployeeRecordBook.Configurations;
using EmployeeRecordBook.Infrastructure.Data;
using EmployeeRecordBook.Infrastructure.Repositories;

Console.WriteLine("Hello, World!");

#region Configure and Register AutoMapper
var config = new MapperConfiguration(config => config.AddProfile(new AutoMapperProfile()));
IMapper mapper = config.CreateMapper();

#endregion



using (var employeeContext = new EmployeeContext())
{
    IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);

    Console.WriteLine("\nsorted employee data:");
    var employees = await employeeRepository.GetEmployeesAsync(pageIndex: 1, pageSize: 4, sortOrder: "asc", sortField: "Name");
    foreach (var empData in employees)
    {
        Console.WriteLine($"{empData.Name}\t{empData.Email}\t{empData.Id}\t{empData.Salary}\t{empData.DepartmentName}");
    }

}
