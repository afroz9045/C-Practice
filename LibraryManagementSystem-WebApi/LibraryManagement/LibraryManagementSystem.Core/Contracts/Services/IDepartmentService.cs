using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IDepartmentService
    {
        Department? AddDepartmentAsync(Department department);

        Department? UpdateDepartmentAsync(Department? existingDepartment, Department updatedDepartment);
    }
}