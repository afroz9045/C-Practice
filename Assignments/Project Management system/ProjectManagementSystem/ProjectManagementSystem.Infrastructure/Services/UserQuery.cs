using Serilog;
using static ProjectManagementSystem.Infrastructure.Data.Constants;
using static ProjectManagementSystem.Infrastructure.Validations.Validations;

namespace ProjectManagementSystem.Infrastructure.Services
{

    public static  class UserQuery
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
                        //var deptId = Convert.ToInt32(Console.ReadLine());
                        //var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
                        //if (DepartmentIdValidate(deptId))
                        //{
                        //    Console.WriteLine("\n\ndepartment wise details by department id:\n\n");
                        //    Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                        //    ProjectManagement.GetSpecificDetails(departmentWiseDetails);
                        //    break;
                        //}
                        //if (validate(departmentWiseDetails))
                        //{
                        //    Console.WriteLine("\n\ndepartment wise details by department id:\n\n");
                        //    Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                        //    ProjectManagement.GetSpecificDetails(departmentWiseDetails);
                        //    break;
                        //}
                        Department();
                        //if (Department())
                        //{
                        //    ////Console.WriteLine("\n\ndepartment wise details by department id:\n\n");
                            ////Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                            ////ProjectManagement.GetSpecificDetails(departmentWiseDetails);
                        //}

                        //Log.Debug($"Invalid department Id {deptId}");
                        //throw new Exception();

                    }
                    catch(NullReferenceException n)
                    {
                        Log.Debug($"Entered input is null");
                        Console.WriteLine(n.Message);
                    }
                    catch(FormatException f)
                    {
                        Log.Debug($"Entered input is of not applicable here");
                        Console.WriteLine(f.Message);
                    }
                    catch(ArgumentOutOfRangeException e)
                    {
                        Log.Debug($"Entered input is out of range");
                        Console.WriteLine(e.Message);
                    }
                    catch(InvalidDataException e)
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

                //case 2:
                //    //Console.WriteLine("Enter department name:");
                //    //string? deptName = Console.ReadLine();
                //    try
                //    {

                        
                //        //if (validate(detailsByDepartmentName))
                //        //{
                //        //    Console.WriteLine("\n\ndepartment wise details by department name:\n\n");
                //        //    Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                //        //    ProjectManagement.GetSpecificDetails(detailsByDepartmentName);
                //        //    break;
                //        //}
                //        //Log.Debug($"Invalid Input {deptName}");
                //        //throw new Exception();
                //    }
                //    catch (NullReferenceException n)
                //    {
                //        Log.Debug($"Entered input is null");
                //        Console.WriteLine(n.Message);
                //    }
                //    catch (FormatException f)
                //    {
                //        Log.Debug($"Entered input is of not applicable here");
                //        Console.WriteLine(f.Message);
                //    }
                //    catch (Exception e)
                //    {
                //        Log.Debug($"Invalid Input {e.Message}");
                //        Console.WriteLine(e.Message);
                //    }
                //    break;
                case 2:
                    //Console.WriteLine("Query you search by inputing:");
                    //var searchKeyword = Console.ReadLine();
                    try
                    {
                        //if (string.IsNullOrWhiteSpace(searchKeyword))
                        //{
                        //    Log.Debug("Invalid input");
                        //    throw new NullReferenceException("Search keyword can't be null or whitespace\n");
                        //}
                        //Console.WriteLine($"\t\t{DepartmentName}\t\t{ProjectName}\t\t\t{AssignmentName}\t\t{EmployeeName}\n");
                        //data.SearchEntity(searchKeyword.ToLower());
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
                //case 4:
                //    //var departmentsDetails = data.GetDepartment();
                //    //Console.WriteLine("\t--------------------- Department Details: ---------------------\n");
                //    //Console.WriteLine($"{DepartmentId}\t\t{PhoneNumber}\t\t{DepartmentName}");
                //    //ProjectManagement.GetDetails(departmentsDetails);
                //    //break;
                //case 5:
                //    try
                //    {
                //        //Console.WriteLine("Enter Department id:");
                //        //var departmentId = Convert.ToInt32(Console.ReadLine());
                //        //var departmentDetailsById = data.GetDepartment(deptId: departmentId);
                //        //if (validate(departmentDetailsById))
                //        //{
                //        //    Console.WriteLine("\n\n\t--------------------- Department Details By Id: ------------------\n");
                //        //    Console.WriteLine($"\t{DepartmentId}\t{PhoneNumber}\t\t{DepartmentName}\n");
                //        //    ProjectManagement.GetDetails(departmentDetailsById);
                //        //    break;
                //        //}
                //        //Log.Debug($"Invalid Input {departmentId}");
                //        //throw new Exception("Invalid input");
                //    }
                //    catch (Exception e)
                //    {
                //        Log.Debug(e.Message);
                //    }
                //    break;
                //case 6:
                //    //Console.WriteLine("Enter department name");
                //    //var departmentName = Console.ReadLine();
                //    //var departmentDetailsUsingName = data.GetDepartment(deptName: departmentName);
                //    try
                //    {
                //        //if (validate(departmentDetailsUsingName))
                //        //{
                //        //    Console.WriteLine("\n\n\t--------------------- Department Details By Name: ---------------------\n");
                //        //    Console.WriteLine($"\t{DepartmentId}\t{PhoneNumber}\t\t{DepartmentName}\n");
                //        //    ProjectManagement.GetDetails(departmentDetailsUsingName);
                //        //    break;
                //        //}
                //        //Log.Debug($"Invalid input {departmentName}");
                //        //throw new Exception("Invalid input");

                //    }
                //    catch (Exception e)
                //    {
                //        Log.Debug(e.Message);
                //    }
                //    break;
                case 7:
                    var projectDetails = data.GetProject();
                    Console.WriteLine("\n\n\t********************************* Project Details: *********************************\n");
                    Console.WriteLine($"{ProjectId}\t{DepartmentId}\t{MaxHours}\t{StartDate}\t\t{EndDate}\t{ProjectName}\n");
                    ProjectManagement.GetDetails(projectDetails);
                    break;
                case 8:
                    //Console.WriteLine("Enter department id");
                    try
                    {
                        //var deptIdForProject = Convert.ToInt32(Console.ReadLine());
                        //var projectDetailsByDeptId = data.GetProject(deptIdForProject);
                        //if (validate(projectDetailsByDeptId))
                        //{
                        //    Console.WriteLine("\n\n\t********************************* Project Details By Department Id: *********************************\n");
                        //    Console.WriteLine($"{ProjectId}\t{DepartmentId}\t{MaxHours}\t{StartDate}\t\t{EndDate}\t{ProjectName}\n");
                        //    ProjectManagement.GetDetails(projectDetailsByDeptId);
                        //    break;
                        //}
                        //Log.Debug($"Invalid Input {deptIdForProject}");
                        //throw new Exception();
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
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
                            Console.WriteLine($"{ProjectId}\t{DepartmentId}\t{MaxHours}\t{StartDate}\t\t{EndDate}\t{ProjectName}\n");
                            ProjectManagement.GetDetails(projectsByDepartmentName);
                            break;
                        }
                        Log.Debug($"Invalid Input {departmentNameForProject}");
                        throw new Exception("Invalid input");
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
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
                            Console.WriteLine($"{ProjectId}\t{DepartmentId}\t{MaxHours}\t{StartDate}\t\t{EndDate}\t{ProjectName}\n");
                            ProjectManagement.GetDetails(projectsByProjectName);
                            break;
                        }
                        Log.Debug($"Invalid input {projectNameForProjectDetails}");
                        throw new Exception("Invalid Input");
                    }
                    catch (FormatException f)
                    {
                        Log.Debug(f.Message);
                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
                    }
                    break;
                case 11:
                    var employeeDetails = data.GetEmployees();
                    Console.WriteLine("\n\n\t############################ Employee Details: ############################\n");
                    Console.WriteLine($"{EmployeeNumber}\t{DepartmentId}\t{PhoneNumber}\t{Email}\t\t\t{Salary}\t\t{FirstName}\t{LastName}\n");
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
                            Console.WriteLine($"{EmployeeNumber}\t{DepartmentId}\t{PhoneNumber}\t{Email}\t\t\t{Salary}\t\t{FirstName}\t{LastName}\n");
                            ProjectManagement.GetDetails(employeeFilterData);
                            break;
                        }
                        Log.Debug($"Invalid Input {deptIdForEmpDetails}");
                        throw new Exception("Invalid Input");
                    }
                    catch (Exception e)
                    {

                        Log.Debug(e.Message);
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
                            Console.WriteLine($"{EmployeeNumber}\t{DepartmentId}\t{PhoneNumber}\t{Email}\t\t\t{Salary}\t\t{FirstName}\t{LastName}\n");
                            ProjectManagement.GetDetails(empRecordByEmpId);
                            break;
                        }
                        Log.Debug($"Invalid Input {empIdForEmpRecord}");
                        throw new Exception("Invalid Input");

                    }
                    catch (Exception e)
                    {
                        Log.Debug(e.Message);
                    }
                    break;
                case 14:
                    var assignmentDetails = data.GetAssignments();
                    Console.WriteLine("\n\n\t\tAssignment Details:\n");
                    Console.WriteLine($"{ProjectId}\t{EmployeeNumber}\t{HoursWorked}\n");
                    ProjectManagement.GetDetails(assignmentDetails);
                    break;
                case 15:
                    Console.WriteLine("\n\n\t############################ Getting Number of employees In Each Department: ############################\n");
                    Console.WriteLine($"\t{DepartmentId}\t\t{NumberOfEmployees}");
                    data.GetNumberOfEmployeesInEachDepartment();
                    break;
                case 16:
                    Console.WriteLine("\n\n\t\tGetting Total Salary In Each Department:\n");
                    var totalDepartmentSalary = data.GetTotalSalaryByEachDepartment();
                    Console.WriteLine($"\t{DepartmentId}\t\t{TotalSalary}");
                    ProjectManagement.GetSpecificDetails(totalSalary: totalDepartmentSalary);
                    Console.WriteLine("\n\n");
                    break;
                case 17:
                    var allDetails = data.GetProjectAndAssignmentDetails();
                    Console.WriteLine("\n\nAll details:\n");
                    Console.WriteLine($"\t{DepartmentId}\t\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}");
                    ProjectManagement.GetSpecificDetails(allDetails);
                    break;
                default:
                    Log.Debug("Invalid Input! please enter valid input.");
                    break;
            }
            Log.CloseAndFlush();
        }
        
    }
}

