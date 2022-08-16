using Pms.Core.Entities;
using ProjectManagementSystem.Core.Models;
using System.Collections;

namespace ProjectManagementSystem.Infrastructure.Services
{
    public interface IProjectManagement
    {
        List<Assignment> GetAssignments();
        IEnumerable<Department>? GetDepartment(int? deptId = null, string? deptName = null);
        IEnumerable<Employee> GetEmployees(int? deptId = null, int? empId = null);
        IEnumerable GetNumberOfEmployeesInEachDepartment();
        IEnumerable<Project>? GetProject(int? departmentId = null, string? projectName = null, string? departmentName = null);
        IEnumerable<ProjectResourceDetails>? GetProjectAndAssignmentDetails(string? deptName = null, int? departmentId = null);
        IEnumerable<TotalSalaryByDepartment> GetTotalSalaryByEachDepartment();
        IEnumerable<ProjectResourceDetails> SearchEntity(string searchKeyword);
    }
}