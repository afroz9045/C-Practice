using Serilog;
using static ProjectManagementSystem.Infrastructure.Data.Constants;
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
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(e.Message);
                    }
                    catch (InvalidDataException e)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Invalid Input {e.Message}");
                        Console.WriteLine(e.Message);
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
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
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
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(e.Message);
                    }
                    catch (InvalidDataException e)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Invalid Input {e.Message}");
                        Console.WriteLine(e.Message);
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
                    }
                    catch (FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(e.Message);
                    }
                    catch (InvalidDataException e)
                    {
                        Log.Debug("Entered input is not a correct type");
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Debug($"Invalid Input {e.Message}");
                        Console.WriteLine(e.Message);
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
                default:
                    Log.Debug("Invalid Input! please enter valid input.");
                    break;
            }
            Log.CloseAndFlush();
        }

    }
}

