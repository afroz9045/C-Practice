using ProjectManagementSystem.Infrastructure.Services;
using Serilog;
using static ProjectManagementSystem.Infrastructure.Data.Constants;

namespace ProjectManagementSystem.Infrastructure.Validations
{
    public static class Validations
    {

        public static bool isContinueResult;
        public static bool IsContinue()
        {
            Console.WriteLine("Do you want to continue...? enter 'Y' for yes/ any key for no");
            var selectedChar = Convert.ToChar(Console.ReadLine());
            if (selectedChar == 'Y' || selectedChar == 'y')
            {
                isContinueResult = true;
            }
            else
            {
                isContinueResult = false;
            }
            return isContinueResult;
        }

        public static bool DepartmentIdValidate(int deparmentId)
        {
            ProjectManagement data = new ProjectManagement();
            var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deparmentId);
            bool isValid;
            int number = 0;
            if (!int.TryParse(Convert.ToString(deparmentId), out number))
            {
                return isValid = false;
                Log.Debug($"Invalid department Id {deparmentId}");
                throw new FormatException("Input entered is of another format");
            }
            if (deparmentId <= 0)
            {
                return isValid = false;
                Log.Debug($"Invalid department Id {deparmentId}");
                throw new ArgumentOutOfRangeException("Input entered is out of range");
            }
            if (!validate(departmentWiseDetails))
            {
                return isValid = false;
                Log.Debug($"Invalid department Id {deparmentId}");
                throw new Exception("Invalid input");
            }

            return isValid = true;
        }

        public static bool DepartmentNameValidate(string departmentName)
        {
            bool isValid;
            int number = 0;
            ProjectManagement data = new ProjectManagement();
            var detailsByDepartmentName = data.GetProjectAndAssignmentDetails(departmentName);
            if (int.TryParse(departmentName, out number))
            {
                return isValid = false;
                throw new FormatException("Input entered is of another format");
            }
            if (!validate(detailsByDepartmentName))
            {
                return isValid = false;
                throw new Exception("Invalid input");
            }
            return isValid = true;
        }

        public static void Department()
        {
            bool isValid;
            ProjectManagement data = new ProjectManagement();

            //var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
            //ProjectManagement data = new ProjectManagement();
            Console.WriteLine("Press 1 to filter by department id");
            Console.WriteLine("Press 2 to filter by department name");
            Console.WriteLine("Press 3 to get all department details");
            Console.WriteLine("Press 4 to get particular department by department id");
            Console.WriteLine("Press 5 to get particular department by department department name");

            int? selectedOption = Convert.ToInt32(Console.ReadLine());

            if (selectedOption == null)
            {
                Console.WriteLine("Input can't be null");
            }
            switch (selectedOption)
            {

                case 1:
                    Console.WriteLine("Enter department id:");
                    var deptId = Convert.ToInt32(Console.ReadLine());
                    var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
                    //if (!int.TryParse(Convert.ToString(deptId),out number)){
                    //    isValid = false;
                    //    Log.Debug($"Invalid department Id {deptId}");
                    //    throw new FormatException("Input entered is of another format");
                    //}
                    //if(deptId<=0)
                    //{
                    //    return isValid = false;
                    //    Log.Debug($"Invalid department Id {deptId}");
                    //    throw new ArgumentOutOfRangeException("Input entered is out of range");
                    //}
                    //if (!validate(departmentWiseDetails))
                    //{
                    //    return isValid = false;
                    //    Log.Debug($"Invalid department Id {deptId}");
                    //    throw new Exception("Invalid input");
                    //}
                    if (DepartmentIdValidate(deptId))
                    {
                        Console.WriteLine("\n\ndepartment wise details by department id:\n\n");
                        Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                        ProjectManagement.GetSpecificDetails(departmentWiseDetails);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter department name:");
                    string? deptName = Console.ReadLine();

                    var detailsByDepartmentName = data.GetProjectAndAssignmentDetails(deptName);
                    //if (int.TryParse(deptName,out number))
                    //{
                    //    return isValid = false;
                    //    throw new FormatException("Input entered is of another format");
                    //}
                    //if (!validate(detailsByDepartmentName)) 
                    //{
                    //    return isValid = false;
                    //    throw new Exception("Invalid input");
                    //
                    if (DepartmentNameValidate(deptName))
                    {
                        Console.WriteLine("\n\ndepartment wise details by department name:\n\n");
                        Console.WriteLine($"\t{DepartmentName}\t\t{ProjectName}\t\t{AssignmentName}\t\t{EmployeeName}\n");
                        ProjectManagement.GetSpecificDetails(detailsByDepartmentName);
                    }
                    break;

                case 3:
                    var departmentsDetails = data.GetDepartment();
                    Console.WriteLine("\t--------------------- Department Details: ---------------------\n");
                    Console.WriteLine($"{DepartmentId}\t\t{PhoneNumber}\t\t{DepartmentName}");
                    ProjectManagement.GetDetails(departmentsDetails);
                    break;
                case 4:
                    Console.WriteLine("Enter Department id:");
                    var departmentId = Convert.ToInt32(Console.ReadLine());
                    var departmentDetailsById = data.GetDepartment(deptId: departmentId);
                    if (DepartmentIdValidate(departmentId))
                    {
                        Console.WriteLine("\n\n\t--------------------- Department Details By Id: ------------------\n");
                        Console.WriteLine($"\t{DepartmentId}\t{PhoneNumber}\t\t{DepartmentName}\n");
                        ProjectManagement.GetDetails(departmentDetailsById);

                    }
                    break;
                case 5:
                    Console.WriteLine("Enter department name");
                    var departmentName = Console.ReadLine();
                    var departmentDetailsUsingName = data.GetDepartment(deptName: departmentName);
                    if (DepartmentNameValidate(departmentName))
                    {
                        Console.WriteLine("\n\n\t--------------------- Department Details By Name: ---------------------\n");
                        Console.WriteLine($"\t{DepartmentId}\t{PhoneNumber}\t\t{DepartmentName}\n");
                        ProjectManagement.GetDetails(departmentDetailsUsingName);
                    }
                    break;
                default:
                    Console.WriteLine("Enter valid selection");
                    break;
            }
            //return isValid = true;
        }

        public static void Search()
        {
            ProjectManagement data = new ProjectManagement();
            Console.WriteLine("Query you search by inputing:");
            var searchKeyword = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchKeyword))
            {
                Log.Debug("Invalid input");
                throw new NullReferenceException("Search keyword can't be null or whitespace\n");
            }
            Console.WriteLine($"\t\t{DepartmentName}\t\t{ProjectName}\t\t\t{AssignmentName}\t\t{EmployeeName}\n");
            data.SearchEntity(searchKeyword.ToLower());
        }

        public static void Project()
        {
            ProjectManagement data = new ProjectManagement();
            Console.WriteLine("Press 1 to get project details by department id");
            Console.WriteLine("Press 2 to get project details by project name");

            int? selectedOption = Convert.ToInt32(Console.ReadLine());

            if (selectedOption == null)
            {
                Console.WriteLine("Input can't be null");
            }
            switch (selectedOption)
            {
                case 1:
                    Console.WriteLine("Enter department id");
                    var deptIdForProject = Convert.ToInt32(Console.ReadLine());
                    var projectDetailsByDeptId = data.GetProject(deptIdForProject);
                    if (DepartmentIdValidate(deptIdForProject))
                    {
                        Console.WriteLine("\n\n\t********************************* Project Details By Department Id: *********************************\n");
                        Console.WriteLine($"{ProjectId}\t{DepartmentId}\t{MaxHours}\t{StartDate}\t\t{EndDate}\t{ProjectName}\n");
                        ProjectManagement.GetDetails(projectDetailsByDeptId);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter department name");
                    break;
                default:
                    break;
            }


        }
        public static bool validate<T>(IEnumerable<T> collection)
        {
            if (collection.Any())
            {
                return true;
            }
            return false;
        }
    }
}
