using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public DepartmentRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<IEnumerable<Department>?> GetDepartmentsAsync()
        {
            var getDepartmentQuery = "select * from [department]";
            var departmentData = await _dapperConnection.QueryAsync<Department>(getDepartmentQuery);
            if (departmentData != null)
                return departmentData;
            return null;
        }

        public async Task<Department?> GetDepartmentByIdAsync(short deptId)
        {
            var getDepartmentByIdQuery = "select * from [department] where deptId = @deptId";
            var department = await _dapperConnection.QueryFirstOrDefaultAsync<Department>(getDepartmentByIdQuery, new { deptId = deptId });
            if (department != null)
                return department;
            return null;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string departmentName)
        {
            var getDepartmentByNameQuery = "select * from [department] where DepartmentName = @departmentName";
            var department = await _dapperConnection.QueryFirstOrDefaultAsync<Department>(getDepartmentByNameQuery, new { departmentName });
            if (department != null)
                return department;
            return null;
        }

        public async Task<Department?> AddDepartmentAsync(Department department)
        {
            var addedDepartment = _libraryDbContext.Departments.Add(department);
            if (addedDepartment != null)
            {
                await _libraryDbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<Department?> UpdateDepartmentAsync(Department department)
        {
            var updatedDepartment = _libraryDbContext.Update(department);
            if (updatedDepartment != null)
            {
                await _libraryDbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }

        public async Task<Department?> DeleteDepartmentAsync(Department department)
        {
            var deletedDepartment = _libraryDbContext.Departments?.Remove(department);
            if (deletedDepartment != null)
            {
                await _libraryDbContext.SaveChangesAsync();
                return department;
            }
            return null;
        }
    }
}