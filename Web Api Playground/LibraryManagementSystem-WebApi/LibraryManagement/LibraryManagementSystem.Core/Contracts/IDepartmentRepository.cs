using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department department);

        Task<Department> DeleteDepartmentAsync(short departmentId);

        Task<dynamic> GetDepartmentByIdAsync(short departmentId);

        Task<IEnumerable<dynamic>> GetDepartmentsAsync();

        Task<Department> UpdateDepartmentAsync(short departmentId, DepartmentDto department);
    }
}