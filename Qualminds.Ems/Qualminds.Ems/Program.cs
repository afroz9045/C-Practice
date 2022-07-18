using Qualminds.Ems.Core.Contracts.Infrastructure;
using Qualminds.Ems.Core.Entities;
using Qualminds.Ems.Infrastructure.IO;
using System.Text;
using System.Text.Json;

char SelectedOption;
string? AddMoreOption;


Console.WriteLine("Enter path:");
string directoryPath = Console.ReadLine();
string fileName = "Employee.csv";
IEmployeeService employeeService = new EmployeeService(Path.Combine(directoryPath, fileName));

Console.WriteLine("Do you want to add employee details...? \n\t\t\tpress 'y' for yes or 'n' for no");
SelectedOption = Convert.ToChar(Console.ReadLine());

do
{
    if (SelectedOption == 'y')
    {
        Console.WriteLine("Enter employee name:");
        var EmpName = Console.ReadLine();
        Console.WriteLine("Enter employee designation:");
        var EmpDesignation = Console.ReadLine();
        IEmployeeService employeeServiceAdd = new EmployeeService(Path.Combine(directoryPath, fileName));
        employeeServiceAdd.AddEmployee(new Employee { Name = EmpName, Designation = EmpDesignation });
       
    }
    else if (SelectedOption == 'n')
    {
        break;
    }
    Console.WriteLine("Do you want to add more:\n\t\t press 'yes' for continue and 'no' for exit");
    AddMoreOption = Console.ReadLine();
} while (AddMoreOption == "yes");


Console.WriteLine($"\nCreated {fileName} with predefined headers");

Console.WriteLine("\nList of all Employees:\n");
StringBuilder employees = employeeService.GetEmployees();
Console.WriteLine(employees);
//var stringifiedEmployees = JsonSerializer.Serialize(employees);

//Console.WriteLine(stringifiedEmployees);



