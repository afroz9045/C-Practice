using Serilog;
using static ProjectManagementSystem.Infrastructure.Validations.Validations;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public static class UserQuery
    {
        public static void MainMenu()
        {
            int option;
            do
            {
                Console.WriteLine("\n\nPlease Select Any One Option:\n");
                Console.WriteLine("Enter 1 for Department Details");
                Console.WriteLine("Enter 2 to Search");
                Console.WriteLine("Enter 3 for Project Details");
                Console.WriteLine("Enter 4 for Employee Details");
                Console.WriteLine("Enter 5 to Get Assignment Details");
                Console.WriteLine("Enter 6 to Get Number of Employees in Each Department");
                Console.WriteLine("Enter 7 to Get Total Salary In Each Department");
                Console.WriteLine("Enter 8 to Get All Details of Projects and Assignment");
                Console.WriteLine("Enter 9 to Clear Console");
                Console.WriteLine("Enter 10 to Exit\n");

                try
                {

                    var selectedOption = Console.ReadLine();
                    if (!int.TryParse(selectedOption, out option))
                    {
                        throw new FormatException("Invalid Input format!");
                    }
                    option = int.Parse(selectedOption);
                    SelectOptions(option);
                    IsContinue();
                }
                catch (FormatException f)
                {

                    Console.WriteLine(f.Message);
                }
            } while (isContinueResult);

        }
        public static void SelectOptions(int selectedOption)
        {
            ProjectManagement data = new ProjectManagement();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/logFile.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            switch (selectedOption)
            {
                case 1:
                    try
                    {
                        Department();
                    }
                    catch (NullReferenceException n)
                    {
                        Log.Debug($"Entered input is null");
                        Console.WriteLine(n.Message);
                        IsInputContinue("department");
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                        IsInputContinue("department");
                    }
                    catch (ArgumentOutOfRangeException a)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(a.Message);
                        IsInputContinue("department");
                    }
                    catch (InvalidDataException i)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(i.Message);
                        IsInputContinue("department");
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                        IsInputContinue("department");
                    }
                    break;
                case 2:
                    try
                    {
                        Search();
                    }
                    catch (NullReferenceException n)
                    {
                        Log.Debug(n.Message);
                        IsInputContinue("search");
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
                        IsInputContinue("search");
                    }
                    break;
                case 3:
                    try
                    {
                        Project();
                    }
                    catch (NullReferenceException n)
                    {
                        Log.Debug($"Entered input is null");
                        Console.WriteLine(n.Message);
                        IsInputContinue("project");
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                        IsInputContinue("project");
                    }
                    catch (ArgumentOutOfRangeException a)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(a.Message);
                        IsInputContinue("project");
                    }
                    catch (InvalidDataException i)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(i.Message);
                        IsInputContinue("project");
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Invalid Input {e.Message}");
                        Console.WriteLine(e.Message);
                        IsInputContinue("project");
                    }
                    break;
                case 4:
                    try
                    {
                        Employee();
                    }
                    catch (NullReferenceException n)
                    {
                        Log.Debug($"Entered input is null");
                        Console.WriteLine(n.Message);
                        IsInputContinue("employee");
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                        IsInputContinue("employee");
                    }
                    catch (ArgumentOutOfRangeException a)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(a.Message);
                        IsInputContinue("employee");
                    }
                    catch (InvalidDataException i)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(i.Message);
                        IsInputContinue("employee");
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"{e.Message}");
                        Console.WriteLine(e.Message);
                        IsInputContinue("employee");
                    }
                    break;
                case 5:
                    Assignment();
                    break;
                case 6:
                    TotalNumberOfEmployeesInEachDepartment();
                    break;
                case 7:
                    TotalSalariesInEachDepartment();
                    break;
                case 8:
                    ProjectsAndAssignment();
                    break;
                case 9:
                    Console.Clear();
                    break;
                case 10:
                    Environment.Exit(0);
                    break;
                default:
                    Log.Debug("Invalid Input! please enter valid input.");
                    break;
            }
            Log.CloseAndFlush();
        }

    }
}

