using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>?> GetDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();
            if (departments != null)
                return departments;
            return null;
        }

        public async Task<Department?> GetDepartmentByIdAsync(short deptId)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(deptId);
            if (department != null)
                return department;
            return null;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string departmentName)
        {
            var department = await _departmentRepository.GetDepartmentByNameAsync(departmentName);
            if (department != null)
                return department;
            return null;
        }

        public async Task<Department?> AddDepartmentAsync(Department department)
        {
            var isDepartmentAlreadyAvailable = await GetDepartmentByNameAsync(department.DepartmentName);
            if (isDepartmentAlreadyAvailable == null)
            {
                var departmentRecord = new Department()
                {
                    DepartmentName = department.DepartmentName,
                    DeptId = department.DeptId
                };
                var addedDepartment = await _departmentRepository.AddDepartmentAsync(departmentRecord);
                if (addedDepartment != null)
                    return addedDepartment;
            }
            return null;
        }

        public async Task<Department?> UpdateDepartmentAsync(short departmentId, Department department)
        {
            var departmentRecord = await GetDepartmentByIdAsync(departmentId);
            if (departmentRecord != null)
            {
                departmentRecord.DeptId = departmentId;
                departmentRecord.DepartmentName = department.DepartmentName;
                var updatedDepartmentRecord = await _departmentRepository.UpdateDepartmentAsync(departmentRecord);
                if (updatedDepartmentRecord != null)
                    return updatedDepartmentRecord;
            }
            return null;
        }

        public async Task<Department?> DeleteDepartmentAsync(short departmentId)
        {
            var departmentToBeDelete = await GetDepartmentByIdAsync(departmentId);
            if (departmentToBeDelete != null)
            {
                var deletedDepartment = await _departmentRepository.DeleteDepartmentAsync(departmentToBeDelete);
                if (deletedDepartment != null)
                    return deletedDepartment;
            }
            return null;
        }
    }
}