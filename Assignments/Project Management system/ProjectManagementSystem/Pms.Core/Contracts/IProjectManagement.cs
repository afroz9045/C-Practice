using Pms.Core.Entities;
using System.Collections;

namespace ProjectManagementSystem.Core.Contracts
{
    public interface IProjectManagement
    {
        public List<Department> GetDepartments();
        public List<Project> GetProjects();
        public List<Employee> GetEmployees();
        public List<Assignment> GetAssignments();
        public List<Project> GetProjectsByDepartmentName(string departmentName);
        public IEnumerable<Employee> GetEmployeesByDeptId(int deptId);
    }
}
