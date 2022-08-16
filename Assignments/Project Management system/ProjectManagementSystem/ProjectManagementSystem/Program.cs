using static ProjectManagementSystem.Infrastructure.Services.UserQuery;
using static ProjectManagementSystem.Infrastructure.Validations.Validations;
using Serilog;


do
{
    Console.WriteLine("\n\nPlease select any one option:\n");
    Console.WriteLine("Enter 1 for Department details");
    Console.WriteLine("Enter 2 to search");
    Console.WriteLine("Enter 3 for project details");
    Console.WriteLine("Enter 4 for Employee details");
    Console.WriteLine("Enter 5 to get Assignment Details");
    Console.WriteLine("Enter 6 to get Number of employees In Each Department");
    Console.WriteLine("Enter 7 to get Total Salary In Each Department");
    Console.WriteLine("Enter 8 to get all details of projects and assignment");
    Console.WriteLine("Enter 9 to clear console");
    Console.WriteLine("Enter 10 to exit\n");

    var selectedOption = Convert.ToInt32(Console.ReadLine());

    SelectOptions(selectedOption);
    IsContinue();
} while (isContinueResult);


