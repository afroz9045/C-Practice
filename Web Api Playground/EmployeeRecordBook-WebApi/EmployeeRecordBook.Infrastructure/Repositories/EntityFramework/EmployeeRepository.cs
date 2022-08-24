using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Core.Infrastructure.Repositories;
using EmployeeRecordBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;

namespace EmployeeRecordBook.Infrastructure.Repositories.EntityFramework
{
   public class EmployeeRepository : IEmployeeRepository
   {
      private readonly EmployeeContext _employeeContext;
      public EmployeeRepository(EmployeeContext employeeContext)
      {
         _employeeContext = employeeContext;
      }

        private readonly IDbConnection _dbConnection;
        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<Employee> CreateAsync(Employee employee)
      {
         _employeeContext.Employees.Add(employee);
         await _employeeContext.SaveChangesAsync();
         return employee;
      }
      public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
      {
         var employeeQuery = (from employee in _employeeContext.Employees.Include(e => e.Department)
                              select new EmployeeDto
                              {
                                 Id = employee.Id,
                                 Name = employee.Name,
                                 Salary = employee.Salary,
                                 DepartmentName = employee.Department.Name
                              }).AsNoTracking();
         //return await _employeeContext.Employees.ToListAsync();
         return await employeeQuery.ToListAsync();  // Executes DB Query in DB and Get results.
      }
      public async Task<Employee> GetEmployeeAsync(int employeeId)
      {
         return await _employeeContext.Employees.FindAsync(employeeId);
      }
      public async Task<Employee> UpdateAsync(int employeeId, Employee employee)
      {
         var employeeToBeUpdated = await GetEmployeeAsync(employeeId);
         //var employeeToBeUpdated = new Employee { Id = employeeId };
         employeeToBeUpdated.Name = employee.Name;
         employeeToBeUpdated.Email = employee.Email;
         employeeToBeUpdated.Salary = employee.Salary;
         employeeToBeUpdated.DepartmentId = employee.DepartmentId;
         //_employeeContext.Entry(employeeToBeUpdated).State = EntityState.Modified;
         _employeeContext.Employees.Update(employeeToBeUpdated);
         _employeeContext.SaveChanges();  // Actual execution of the command happens here with DB.
         return employeeToBeUpdated;
      }
      public async Task DeleteAsync(int employeeId)
      {
         var employeeToBeDeleted = await GetEmployeeAsync(employeeId);
         _employeeContext.Employees.Remove(employeeToBeDeleted);
         await _employeeContext.SaveChangesAsync();
      }

        public async Task<IEnumerable<EmployeeMinimumData>> GetEmployeesByView()
        {
            var viewQuery = "Select * from vEmployeeRecord";
            return await _dbConnection.QueryAsync<EmployeeMinimumData>(viewQuery);

        }

       
    }
}
