// See https://aka.ms/new-console-template for more information
using EntityFrameworkPlayground.core.Entities;
using EntityFrameworkPlayground.core.Infrastructure.Repositories;
using EntityFrameworkPlayground.Infrastructure.Data;
using EntityFrameworkPlayground.Infrastructure.Repositories;

Console.WriteLine("Entity Framework Playground!");

//var departmentRepository = new DepartmentRepository();

//departmentRepository.Create(new Department() { DepartmentName = "HR"});
//departmentRepository.Create(new Department() { DepartmentName = "IT"});
//departmentRepository.Create(new Department() { DepartmentName = "Accounting"});

using(var employeeContext = new EmployeeContext())
{
    IEmployeeRepository employeeRespository = new EmployeeRepository(employeeContext);
    var parmeshwar = await employeeRespository.CreateAsync(new Employee
    {
        EmployeeName = "Parmeshwar",
        Email = "parmeshwar@gmail.com",
        Salary = 15000m,
        DepartmentId = 1
    });
    var parvez = await employeeRespository.CreateAsync(
        new Employee
        {
            EmployeeName = "Parvez",
            Email = "parvez@gmail.com",
            Salary = 15000m,
            DepartmentId = 1
        }
  );

    Console.WriteLine($"Created Employees: {parmeshwar.EmployeeId} {parmeshwar.EmployeeName} {parmeshwar.Salary}");

    var employees = await employeeRespository.GetEmployeesAsync();
    Console.WriteLine($"Total Employees Records: {employees.Count()}");


}

