using Pms.Core.Entities;

namespace ProjectManagementSystem.Core.Contracts
{
    public interface IProjectManagement
    {
        public List<Department> GetDepartments();
        public List<Project> GetProjects();
        public List<Employee> GetEmployees();
        public List<Assignment> GetAssignments();
        public List<Project> GetProjectsByDepartmentName(string departmentName);
    }
}
