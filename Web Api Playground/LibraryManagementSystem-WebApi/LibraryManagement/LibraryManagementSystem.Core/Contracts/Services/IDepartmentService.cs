using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IDepartmentService
    {
        Task<Department?> AddDepartmentAsync(Department department);
        Task<Department?> DeleteDepartmentAsync(short departmentId);
        Task<Department?> GetDepartmentByIdAsync(short deptId);
        Task<Department?> GetDepartmentByNameAsync(string departmentName);
        Task<IEnumerable<Department>?> GetDepartmentsAsync();
        Task<Department?> UpdateDepartmentAsync(short departmentId, Department department);
    }
}