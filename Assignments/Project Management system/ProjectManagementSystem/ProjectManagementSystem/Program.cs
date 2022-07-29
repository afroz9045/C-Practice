using ProjectManagementSystem.Infrastructure.Services;

const string Project_Id = "Project Id";
const string Employee_Number = "EmployeeNumber";
const string Hours_Worked = "Hours Worked";

ProjectManagement data = new ProjectManagement();
var departmentsDetails = data.GetDepartments();
var projectDetails = data.GetProjects();
var assignmentDetails = data.GetAssignments();
var employeeDetails = data.GetEmployees();


var departmentDetailsById = data.GetDepartments(2);

var departmentDetailsByName = data.GetDepartments("Marketing");

var projectDetailsByDeptId = data.GetProjects(1);



Console.WriteLine("\t\t\tDepartment Details:\n");
ProjectManagement.GetDetails(departmentsDetails);
Console.WriteLine("\n\n\t\tProject Details:\n");
ProjectManagement.GetDetails(projectDetails);
Console.WriteLine("\n\n\t\tAssignment Details:\n");
Console.WriteLine($"{Project_Id}\t{Employee_Number}\t{Hours_Worked}\n");
ProjectManagement.GetDetails(assignmentDetails);
Console.WriteLine("\n\n\t\tEmployee Details:\n");
ProjectManagement.GetDetails(employeeDetails);
Console.WriteLine("\n\n\t\tDepartment Details By Id:\n");
ProjectManagement.GetDetails(departmentDetailsById);
Console.WriteLine("\n\n\t\tDepartment Details By Name:\n");
ProjectManagement.GetDetails(departmentDetailsByName);
Console.WriteLine("\n\n\t\tProject Details By Department Id:\n");
ProjectManagement.GetDetails(projectDetailsByDeptId);

Console.WriteLine("\n\n\t\tGetting project by department name:\n");
var projectsByDepartmentName = data.GetProjectsByDepartmentName("Marketing");
ProjectManagement.GetSpecificDetails(projectsByDepartmentName);
//foreach (var projectData in projectsByDepartmentName)
//{
//    Console.WriteLine(projectData);
//}
Console.WriteLine("\n\n\t\tEmployee filter data:\n");

var employeeFilterData = data.GetEmployeesByDeptId(1);
ProjectManagement.GetSpecificDetails(employeeFilterData);



