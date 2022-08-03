using ProjectManagementSystem.Infrastructure.Services;

const string Project_Id = "Project Id";
const string Employee_Number = "EmployeeNumber";
const string Hours_Worked = "Hours Worked";

bool isContinueResult;

ProjectManagement data = new ProjectManagement();



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
    Console.WriteLine("Enter 17 to get all details");



    var selectedOption = Convert.ToInt32(Console.ReadLine());
  
    bool IsContinue()
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

    bool typeCheckInt(string input)
    {
        int number = 0;
        return int.TryParse(input, out number);
    }

    void speacialExpressionCheck(string data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (!(char.IsLetterOrDigit(data, i)))
            {
                 throw new FormatException();
            }
        }

    }

    void departmentIdValidate(int deptId)
    {
        if (deptId < 0 || deptId > 3)
        {
            throw new InvalidOperationException("Invalid selection please enter valid input!");
        }
        if (!typeCheckInt(Convert.ToString(deptId)))
        {
            throw new FormatException();
        }
    }

    void departmentNameValidate(string deptName)
    {
        if (string.IsNullOrWhiteSpace(deptName) || typeCheckInt(deptName))
        {
            throw new InvalidOperationException("Invalid input! please enter valid input");
        }
        speacialExpressionCheck(deptName);
    }



    switch (selectedOption)
    {
        case 1:
            Console.WriteLine("Enter department id:");
            try
            {
                int deptId = Convert.ToInt32(Console.ReadLine());
                departmentIdValidate(deptId);
                var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
                Console.WriteLine("\n\ndepartment wise details by department id:\n");
                ProjectManagement.GetSpecificDetails(departmentWiseDetails);

            }

            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;

        case 2:
            Console.WriteLine("Enter department name:");
            string? deptName = Console.ReadLine();

            try
            {
                departmentNameValidate(deptName);

                var detailsByDepartmentName = data.GetProjectAndAssignmentDetails(deptName.ToLower());
                if (detailsByDepartmentName != null)
                {
                    Console.WriteLine("\n\ndepartment wise details by department name:\n");
                    ProjectManagement.GetSpecificDetails(detailsByDepartmentName);
                }
                break;
            }
            catch (InvalidOperationException i)
            {

                Console.WriteLine(i.Message);
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            break;
        case 3:
            Console.WriteLine("Query you search by inputing:");
            var searchKeyword = Console.ReadLine();
            data.SearchEntity(searchKeyword.ToLower());
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
                departmentIdValidate(departmentId);
                var departmentDetailsById = data.GetDepartment(departmentId);
                Console.WriteLine("\n\n\t--------------------- Department Details By Id: ------------------\n");
                ProjectManagement.GetDetails(departmentDetailsById);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case 6:
            Console.WriteLine("Enter department name");
            var departmentName = Console.ReadLine();
            try
            {
                departmentNameValidate(departmentName);

            }
            catch (InvalidOperationException i)
            {

                Console.WriteLine(i.Message);
            }
            catch (FormatException f)
            {
                Console.WriteLine(f.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            var departmentDetailsUsingName = data.GetDepartment(deptName: departmentName);
            if((departmentDetailsUsingName != null) && (departmentDetailsUsingName.ToString().Contains(departmentName.ToLower()))) { 
            Console.WriteLine("\n\n\t--------------------- Department Details By Name: ---------------------\n");
            ProjectManagement.GetDetails(departmentDetailsUsingName);
            }
            break;
        case 7:
            var projectDetails = data.GetProject();
            Console.WriteLine("\n\n\t********************************* Project Details: *********************************\n");
            ProjectManagement.GetDetails(projectDetails);
            break;
        case 8:
            Console.WriteLine("Enter department id");
            var deptIdForProject = Convert.ToInt32(Console.ReadLine());
            var projectDetailsByDeptId = data.GetProject(deptIdForProject);
            Console.WriteLine("\n\n\t********************************* Project Details By Department Id: *********************************\n");
            ProjectManagement.GetDetails(projectDetailsByDeptId);
            break;
        case 9:
            Console.WriteLine("Enter department name");
            var departmentNameForProject = Console.ReadLine();
            var projectsByDepartmentName = data.GetProject(departmentName: departmentNameForProject);
            Console.WriteLine("\n\n\t********************************* Getting project by department name: *********************************\n");
            ProjectManagement.GetDetails(projectsByDepartmentName);
            break;
        case 10:
            Console.WriteLine("Enter project name");
            var projectNameForProjectDetails = Console.ReadLine();
            var projectsByProjectName = data.GetProject(projectName: projectNameForProjectDetails);
            Console.WriteLine("\n\n\t********************************* Getting project by Project name: *********************************\n");
            ProjectManagement.GetDetails(projectsByProjectName);
            break;
        case 11:
            var employeeDetails = data.GetEmployees();
            Console.WriteLine("\n\n\t############################ Employee Details: ############################\n");
            ProjectManagement.GetDetails(employeeDetails);
            break;
        case 12:
            Console.WriteLine("Enter department id");
            var deptIdForEmpDetails = Convert.ToInt32(Console.ReadLine());
            var employeeFilterData = data.GetEmployees(deptIdForEmpDetails);
            Console.WriteLine("\n\n\t############################ Getting Employee data using Department Id: ############################\n");
            ProjectManagement.GetDetails(employeeFilterData);
            break;
        case 13:
            Console.WriteLine("Enter EmpId");
            var empIdForEmpRecord = Convert.ToInt32(Console.ReadLine());
            var empRecordByEmpId = data.GetEmployees(empId: empIdForEmpRecord);
            Console.WriteLine("\n\n\t############################ Getting Employee Record using Employee Id: ############################\n");
            ProjectManagement.GetDetails(empRecordByEmpId);
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
            Console.WriteLine("\n\nAll details:\n");
            var allDetails = data.GetProjectAndAssignmentDetails();
            ProjectManagement.GetSpecificDetails(allDetails);
            break;
        default:
            Console.WriteLine("Invalid Input! please enter valid input.");
            break;
    }
    IsContinue();
} while (isContinueResult);


