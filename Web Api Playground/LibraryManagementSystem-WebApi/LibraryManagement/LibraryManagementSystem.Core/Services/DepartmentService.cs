using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public class DepartmentService : IDepartmentService
    {
        public Department? AddDepartmentAsync(Department department)
        {
            var departmentRecord = new Department()
            {
                DepartmentName = department.DepartmentName,
                DeptId = department.DeptId
            };
            return departmentRecord;
        }

        public Department? UpdateDepartmentAsync(Department? existingDepartment, Department departmentToBeUpdate)
        {
            if (existingDepartment != null)
            {
                existingDepartment.DepartmentName = departmentToBeUpdate.DepartmentName;
            }
            return existingDepartment;
        }
    }
}