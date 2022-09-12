using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> AddDepartmentAsync(Department department);

        Task<Department?> DeleteDepartmentAsync(Department department);

        Task<Department?> GetDepartmentByIdAsync(short deptId);

        Task<Department?> GetDepartmentByNameAsync(string departmentName);

        Task<IEnumerable<Department>?> GetDepartmentsAsync();

        Task<Department?> UpdateDepartmentAsync(Department department);
    }
}