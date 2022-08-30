using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<dynamic>> GetDepartmentsAsync()
        {
            var getDepartmentQuery = "select * from [department]";
            var departmentData = await _dapperConnection.QueryAsync(getDepartmentQuery);
            return departmentData;
        }

        public async Task<dynamic> GetDepartmentByIdAsync(short departmentId)
        {
            //var getDepartmentByIdQuery = "select * from [department] where deptId = @departmentId";
            //return (await _dapperConnection.QueryAsync(getDepartmentByIdQuery, new { deptId = departmentId })).FirstOrDefault();
            var departmentData = await (from department in _libraryDbContext.Departments
                                        where department.DeptId == departmentId
                                        select new Department()
                                        {
                                            DeptId = department.DeptId,
                                            DepartmentName = department.DepartmentName
                                        }).FirstOrDefaultAsync();
            return departmentData;
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            var departmentRecord = new Department()
            {
                DepartmentName = department.DepartmentName,
                DeptId = department.DeptId,
                //StudentId = department.StudentId
            };
            _libraryDbContext.Departments.Add(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }

        public async Task<Department> UpdateDepartmentAsync(short departmentId, DepartmentDto department)
        {
            var departmentRecord = await GetDepartmentByIdAsync(departmentId);

            departmentRecord.DeptId = department.DeptId;
            //departmentRecord.StudentId = department.StudentId;
            departmentRecord.DepartmentName = department.DepartmentName;

            _libraryDbContext.Update(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }

        public async Task<Department> DeleteDepartmentAsync(short departmentId)
        {
            var departmentRecord = await GetDepartmentByIdAsync(departmentId);
            _libraryDbContext.Books?.Remove(departmentRecord);
            await _libraryDbContext.SaveChangesAsync();
            return departmentRecord;
        }
    }
}