using Pms.Core.Entities;
using System.Collections;

namespace ProjectManagementSystem.Core.Contracts
{
    public interface IProjectManagement
    {
        public IEnumerable<Department> GetDepartment(int? deptId = null, string? deptName = null);
        public IEnumerable<Project> GetProject(int? departmentId, string? projectName, string? departmentName);
        public IEnumerable<Employee> GetEmployees(int? deptId = null, int? empId = null);
        public List<Assignment> GetAssignments();
    }
}
