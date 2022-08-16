using Serilog;
using static ProjectManagementSystem.Infrastructure.Validations.Validations;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public static class UserQuery
    {
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

