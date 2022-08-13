using Pagging.Infrastructure.Data;
using Pagging.Infrastructure;

var empData = InMemoryData.GetEmployees(3, 2);
foreach (var employee in empData)
{
    Console.WriteLine($"{employee.EmpId} {employee.EmpName}");
}