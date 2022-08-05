using static ProjectManagementSystem.Infrastructure.Validations.Validations;
using Serilog;

namespace ProjectManagementSystem.Infrastructure.Services
{

    public static  class UserQuery
    {
        const string Project_Id = "Project Id";
        const string Employee_Number = "EmployeeNumber";
        const string Hours_Worked = "Hours Worked";

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
                    Console.WriteLine("Enter department id:");
                    try
                    {
                        var deptId = Convert.ToInt32(Console.ReadLine());
                        var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
                        if (validate(departmentWiseDetails))
                        {
                            Console.WriteLine("\n\ndepartment wise details by department id:\n");
                            ProjectManagement.GetSpecificDetails(departmentWiseDetails);
                            break;
                        }
                        Log.Debug($"Invalid department Id {deptId}");
                        throw new Exception();

                    }
                    catch (Exception e)
                    {
                        Log.Error($"Invalid Input {e.Message}");
                    }
                    break;

                case 2:
                    Console.WriteLine("Enter department name:");
                    string? deptName = Console.ReadLine();
                    try
                    {

                        var detailsByDepartmentName = data.GetProjectAndAssignmentDetails(deptName);
                        if (validate(detailsByDepartmentName))
                        {
                            Console.WriteLine("\n\ndepartment wise details by department name:\n");
                            ProjectManagement.GetSpecificDetails(detailsByDepartmentName);
                            break;
                        }
                        Log.Debug($"Invalid Input {deptName}");
                        throw new Exception();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Invalid Input {e.Message}");
                    }
                    break;
                case 3:
                    Console.WriteLine("Query you search by inputing:");
                    var searchKeyword = Console.ReadLine();
                    try
                    {
                        if (string.IsNullOrWhiteSpace(searchKeyword))
                        {
                            Log.Debug("Invalid input");
                            throw new NullReferenceException("Search keyword can't be null or whitespace\n");
                        }
                        data.SearchEntity(searchKeyword.ToLower());
                    }
                    catch (NullReferenceException n)
                    {
                        Log.Error(n.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 4:
                    var departmentsDetails = data.GetDepartment();
                    Console.WriteLine("\t--------------------- Department Details: ---------------------\n");
                    ProjectManagement.GetDetails(departmentsDetails);
                    break;
                case 5:
                    try
                    {
                        Console.WriteLine("Enter Department id:");
                        var departmentId = Convert.ToInt32(Console.ReadLine());
                        var departmentDetailsById = data.GetDepartment(deptId: departmentId);
                        if (validate(departmentDetailsById))
                        {
                            Console.WriteLine("\n\n\t--------------------- Department Details By Id: ------------------\n");
                            ProjectManagement.GetDetails(departmentDetailsById);
                            break;
                        }
                        Log.Debug($"Invalid Input {departmentId}");
                        throw new Exception("Invalid input");
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 6:
                    Console.WriteLine("Enter department name");
                    var departmentName = Console.ReadLine();
                    var departmentDetailsUsingName = data.GetDepartment(deptName: departmentName);
                    try
                    {
                        if (validate(departmentDetailsUsingName))
                        {
                            Console.WriteLine("\n\n\t--------------------- Department Details By Name: ---------------------\n");
                            ProjectManagement.GetDetails(departmentDetailsUsingName);
                            break;
                        }
                        Log.Debug($"Invalid input {departmentName}");
                        throw new Exception("Invalid input");

                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 7:
                    var projectDetails = data.GetProject();
                    Console.WriteLine("\n\n\t********************************* Project Details: *********************************\n");
                    ProjectManagement.GetDetails(projectDetails);
                    break;
                case 8:
                    Console.WriteLine("Enter department id");
                    try
                    {
                        var deptIdForProject = Convert.ToInt32(Console.ReadLine());
                        var projectDetailsByDeptId = data.GetProject(deptIdForProject);
                        if (validate(projectDetailsByDeptId))
                        {
                            Console.WriteLine("\n\n\t********************************* Project Details By Department Id: *********************************\n");
                            ProjectManagement.GetDetails(projectDetailsByDeptId);
                            break;
                        }
                        Log.Debug($"Invalid Input {deptIdForProject}");
                        throw new Exception();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }

                    break;
                case 9:
                    Console.WriteLine("Enter department name");
                    try
                    {
                        var departmentNameForProject = Console.ReadLine();
                        var projectsByDepartmentName = data.GetProject(departmentName: departmentNameForProject.ToLower());
                        if (validate(projectsByDepartmentName))
                        {
                            Console.WriteLine("\n\n\t********************************* Getting project by department name: *********************************\n");
                            ProjectManagement.GetDetails(projectsByDepartmentName);
                            break;
                        }
                        Log.Debug($"Invalid Input {departmentNameForProject}");
                        throw new Exception("Invalid input");
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 10:
                    Console.WriteLine("Enter project name");
                    try
                    {
                        var projectNameForProjectDetails = Console.ReadLine();
                        var projectsByProjectName = data.GetProject(projectName: projectNameForProjectDetails);
                        if (validate(projectsByProjectName))
                        {
                            Console.WriteLine("\n\n\t********************************* Getting project by Project name: *********************************\n");
                            ProjectManagement.GetDetails(projectsByProjectName);
                            break;
                        }
                        Log.Debug($"Invalid input {projectNameForProjectDetails}");
                        throw new Exception("Invalid Input");
                    }
                    catch (FormatException f)
                    {
                        Log.Error(f.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 11:
                    var employeeDetails = data.GetEmployees();
                    Console.WriteLine("\n\n\t############################ Employee Details: ############################\n");
                    ProjectManagement.GetDetails(employeeDetails);
                    break;
                case 12:
                    try
                    {
                        Console.WriteLine("Enter department id");
                        var deptIdForEmpDetails = Convert.ToInt32(Console.ReadLine());
                        var employeeFilterData = data.GetEmployees(deptIdForEmpDetails);
                        if (validate(employeeFilterData))
                        {
                            Console.WriteLine("\n\n\t############################ Getting Employee data using Department Id: ############################\n");
                            ProjectManagement.GetDetails(employeeFilterData);
                            break;
                        }
                        Log.Debug($"Invalid Input {deptIdForEmpDetails}");
                        throw new Exception("Invalid Input");
                    }
                    catch (Exception e)
                    {

                        Log.Error(e.Message);
                    }
                    break;
                case 13:
                    try
                    {
                        Console.WriteLine("Enter EmpId");
                        var empIdForEmpRecord = Convert.ToInt32(Console.ReadLine());
                        var empRecordByEmpId = data.GetEmployees(empId: empIdForEmpRecord);
                        if (validate(empRecordByEmpId))
                        {
                            Console.WriteLine("\n\n\t############################ Getting Employee Record using Employee Id: ############################\n");
                            ProjectManagement.GetDetails(empRecordByEmpId);
                            break;
                        }
                        Log.Debug($"Invalid Input {empIdForEmpRecord}");
                        throw new Exception("Invalid Input");

                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
                    break;
                case 14:
                    var assignmentDetails = data.GetAssignments();
                    Console.WriteLine("\n\n\t\tAssignment Details:\n");
                    Console.WriteLine($"{Project_Id}\t{Employee_Number}\t{Hours_Worked}\n");
                    ProjectManagement.GetDetails(assignmentDetails);
                    break;
                case 15:
                    Console.WriteLine("\n\n\t############################ Getting Number of employees In Each Department: ############################\n");
                    data.GetNumberOfEmployeesInEachDepartment();
                    break;
                case 16:
                    Console.WriteLine("\n\n\t\tGetting Total Salary In Each Department:\n");
                    var totalDepartmentSalary = data.GetTotalSalaryByEachDepartment();
                    ProjectManagement.GetSpecificDetails(totalSalary: totalDepartmentSalary);
                    Console.WriteLine("\n\n");
                    break;
                case 17:
                    var allDetails = data.GetProjectAndAssignmentDetails();
                    Console.WriteLine("\n\nAll details:\n");
                    ProjectManagement.GetSpecificDetails(allDetails);
                    break;
                default:
                    Log.Error("Invalid Input! please enter valid input.");
                    break;
            }
            Log.CloseAndFlush();
        }
        
    }
}

