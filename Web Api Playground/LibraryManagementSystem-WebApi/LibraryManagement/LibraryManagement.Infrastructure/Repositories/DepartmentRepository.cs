using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
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

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var getDepartmentQuery = "select * from [department]";
            var departmentData = await _dapperConnection.QueryAsync<Department>(getDepartmentQuery);
            return departmentData;
        }

        public async Task<Department> GetDepartmentByIdAsync(short deptId)
        {
            var getDepartmentByIdQuery = "select * from [department] where deptId = @deptId";
            return (await _dapperConnection.QueryFirstAsync<Department>(getDepartmentByIdQuery, new { deptId = deptId }));
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            var departmentRecord = new Department()
            {
                DepartmentName = department.DepartmentName,
                DeptId = department.DeptId
            };
            _libraryDbContext.Departments.Add(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }

        public async Task<Department> UpdateDepartmentAsync(short departmentId, Department department)
        {
            var departmentRecord = await GetDepartmentByIdAsync(departmentId);

            departmentRecord.DeptId = departmentId;
            departmentRecord.DepartmentName = department.DepartmentName;

            _libraryDbContext.Update(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }

        public async Task<Department> DeleteDepartmentAsync(short departmentId)
        {
            var departmentRecord = await GetDepartmentByIdAsync(departmentId);
            _libraryDbContext.Departments?.Remove(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }
    }
}