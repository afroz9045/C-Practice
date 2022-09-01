using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department department);

        Task<Department> DeleteDepartmentAsync(short departmentId);

        Task<Department> GetDepartmentByIdAsync(short departmentId);

        Task<IEnumerable<Department>> GetDepartmentsAsync();

        Task<Department> UpdateDepartmentAsync(short departmentId, Department department);
    }
}