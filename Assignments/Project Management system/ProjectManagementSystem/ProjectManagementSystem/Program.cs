using ProjectManagementSystem.Infrastructure.Services;

const string Project_Id = "Project Id";
const string Employee_Number = "EmployeeNumber";
const string Hours_Worked = "Hours Worked";

ProjectManagement data = new ProjectManagement();

var departmentsDetails = data.GetDepartment();
var departmentDetailsById = data.GetDepartment(2);
var departmentDetailsByName = data.GetDepartment(deptName: "Marketing");

var projectDetails = data.GetProject();
var projectDetailsByDeptId = data.GetProject(1);
var projectsByDepartmentName = data.GetProject(departmentName: "Marketing");
var projectsByProjectName = data.GetProject(projectName: "2022 Q1 Tax Preparation");

var employeeDetails = data.GetEmployees();
var employeeFilterData = data.GetEmployees(1);
var empRecordByEmpId = data.GetEmployees(empId: 114);

var assignmentDetails = data.GetAssignments();













Console.WriteLine("\t--------------------- Department Details: ---------------------\n");
ProjectManagement.GetDetails(departmentsDetails);

Console.WriteLine("\n\n\t--------------------- Department Details By Id: ------------------\n");
ProjectManagement.GetDetails(departmentDetailsById);

Console.WriteLine("\n\n\t--------------------- Department Details By Name: ---------------------\n");
ProjectManagement.GetDetails(departmentDetailsByName);



Console.WriteLine("\n\n\t********************************* Project Details: *********************************\n");
ProjectManagement.GetDetails(projectDetails);

Console.WriteLine("\n\n\t********************************* Project Details By Department Id: *********************************\n");
ProjectManagement.GetDetails(projectDetailsByDeptId);

Console.WriteLine("\n\n\t********************************* Getting project by department name: *********************************\n");
ProjectManagement.GetDetails(projectsByDepartmentName);

Console.WriteLine("\n\n\t********************************* Getting project by Project name: *********************************\n");
ProjectManagement.GetDetails(projectsByProjectName);




Console.WriteLine("\n\n\t############################ Employee Details: ############################\n");
ProjectManagement.GetDetails(employeeDetails);

Console.WriteLine("\n\n\t############################ Getting Employee data using Department details: ############################\n");
ProjectManagement.GetDetails(employeeFilterData);

Console.WriteLine("\n\n\t############################ Getting Employee Record using Employee Id: ############################\n");
ProjectManagement.GetDetails(empRecordByEmpId);

Console.WriteLine("\n\n\t############################ Getting Number of employees In Each Department: ############################\n");
data.GetNumberOfEmployeesInEachDepartment();



Console.WriteLine("\n\n\t\tAssignment Details:\n");
Console.WriteLine($"{Project_Id}\t{Employee_Number}\t{Hours_Worked}\n");
ProjectManagement.GetDetails(assignmentDetails);









Console.WriteLine("\n\n\t\tGetting Total Salary In Each Department:\n");
var totalDepartmentSalary = data.GetTotalSalaryByEachDepartment();
ProjectManagement.GetSpecificDetails(totalSalary: totalDepartmentSalary);
Console.WriteLine("\n\n");

Console.WriteLine("\n\nAll details:\n");
var allDetails = data.GetProjectAndAssignmentDetails();
ProjectManagement.GetSpecificDetails(allDetails);

Console.WriteLine("\n\nPlease select any one option:\n");
Console.WriteLine("press 1 to filter with Department Id");
Console.WriteLine("press 2 to filter with Department Name");
Console.WriteLine("press 3 to search");

var selectedOption = Convert.ToInt32(Console.ReadLine());




switch (selectedOption)
{
    case 1:
        Console.WriteLine("Enter department id:");
        try
        {
            int deptId = Convert.ToInt32(Console.ReadLine());
            if (deptId < 0 || deptId > 3)
            {
                throw new InvalidOperationException("Invalid selection please enter valid input!");
            }
            var departmentWiseDetails = data.GetProjectAndAssignmentDetails(departmentId: deptId);
            Console.WriteLine("\n\ndepartment wise details by department id:\n");
            ProjectManagement.GetSpecificDetails(departmentWiseDetails);

        }

        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
        break;

    case 2:
        Console.WriteLine("Enter department name:");
        string? deptName = Console.ReadLine();
        try
        {
            if (string.IsNullOrEmpty(deptName) || deptName != department.)
            {
                throw new InvalidOperationException("Invalid input! please enter valid input");
            }
            var detailsByDepartmentName = data.GetProjectAndAssignmentDetails(deptName.ToLower());
            Console.WriteLine("\n\ndepartment wise details by department name:\n");
            ProjectManagement.GetSpecificDetails(detailsByDepartmentName);
        }
        catch (InvalidOperationException i)
        {

            Console.WriteLine(i.Message);
        }
     
        break;
    case 3:
        Console.WriteLine("Query you search by inputing:");
        var searchKeyword = Console.ReadLine();
        data.SearchEntity(searchKeyword.ToLower());
        break;
    default:
        Console.WriteLine("Invalid Input! please enter valid input.");
        break;
}











//Finding 
//Console.WriteLine("\n\nSearching:\n");
//data.SearchEntity("Ma");


