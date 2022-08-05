using static ProjectManagementSystem.Infrastructure.Services.UserQuery;
using static ProjectManagementSystem.Infrastructure.Validations.Validations;
using Serilog;






Log.Information("Started program execution.");
do
{

    Console.WriteLine("\n\nPlease select any one option:\n");
    Console.WriteLine("Enter 1 to filter with Department Id");
    Console.WriteLine("Enter 2 to filter with Department Name");
    Console.WriteLine("Enter 3 to search");
    Console.WriteLine("Enter 4 to get department details");
    Console.WriteLine("Enter 5 to get department details by department id");
    Console.WriteLine("Enter 6 to get department details by department name");
    Console.WriteLine("Enter 7 to get project details");
    Console.WriteLine("Enter 8 to get Project Details By Department Id");
    Console.WriteLine("Enter 9 to get project by department name");
    Console.WriteLine("Enter 10 to get project by Project name");
    Console.WriteLine("Enter 11 to get Employee Details");
    Console.WriteLine("Enter 12 to get Employee Record using Department Id");
    Console.WriteLine("Enter 13 to get Employee Record using Employee Id");
    Console.WriteLine("Enter 14 to get Assignment Details");
    Console.WriteLine("Enter 15 to get Number of employees In Each Department");
    Console.WriteLine("Enter 16 to get Total Salary In Each Department");
    Console.WriteLine("Enter 17 to get all details\n");



    var selectedOption = Convert.ToInt32(Console.ReadLine());

    SelectOptions(selectedOption);
    IsContinue();
} while (isContinueResult);


