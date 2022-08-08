using EntityFrameworkPlayground.core.Entities;
using EntityFrameworkPlayground.core.Infrastructure.Repositories;
using EntityFrameworkPlayground.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPlayground.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        // Create Employee
        public async Task<Employee> CreateAsync(Employee employee)
        {
            _employeeContext.Employees.Add(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        // Get Employee
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var employeeQuery = from employee in _employeeContext.Employees
                                select employee;
            return await employeeQuery.ToListAsync();
        }
        public async Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return await _employeeContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        // Update Employee
        public async Task<Employee> UpdateAsync(int employeeId, Employee employee)
        {
            var employeeToBeUpdated = await GetEmployeeAsync(employeeId);
            employeeToBeUpdated.EmployeeName = employee.EmployeeName;
            employeeToBeUpdated.Email = employee.Email;
            employeeToBeUpdated.Salary = employee.Salary;
            employeeToBeUpdated.DepartmentId = employee.DepartmentId;

            _employeeContext.Update(employeeToBeUpdated);
            _employeeContext.SaveChanges();
            return employeeToBeUpdated;
        }


        public async void DeleteAsync(int employeeID)
        {
            var employeeToBeDeleted = await GetEmployeeAsync(employeeID);
            _employeeContext.Employees.Remove(employeeToBeDeleted);
            await _employeeContext.SaveChangesAsync();
        }
    }
}
