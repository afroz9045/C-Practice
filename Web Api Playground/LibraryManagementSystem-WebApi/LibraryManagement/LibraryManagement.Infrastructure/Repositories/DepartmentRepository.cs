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
            return departmentData;
        }

        public async Task<Department?> GetDepartmentByIdAsync(short deptId)
        {
            var getDepartmentByIdQuery = "select * from [department] where deptId = @deptId";
            var department = await _dapperConnection.QueryFirstOrDefaultAsync<Department>(getDepartmentByIdQuery, new { deptId = deptId });
            return department;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string departmentName)
        {
            var getDepartmentByNameQuery = "select * from [department] where DepartmentName = @departmentName";
            var department = await _dapperConnection.QueryFirstOrDefaultAsync<Department>(getDepartmentByNameQuery, new { departmentName });
            return department;
        }

        public async Task<Department?> AddDepartmentAsync(Department department)
        {
            var addedDepartment = _libraryDbContext.Departments.Add(department);
            await _libraryDbContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> UpdateDepartmentAsync(Department department)
        {
            var updatedDepartment = _libraryDbContext.Update(department);
            await _libraryDbContext.SaveChangesAsync();
            return department;
        }

        public async Task<Department?> DeleteDepartmentAsync(Department department)
        {
            var deletedDepartment = _libraryDbContext.Departments?.Remove(department);
            await _libraryDbContext.SaveChangesAsync();
            return department;
        }
    }
}