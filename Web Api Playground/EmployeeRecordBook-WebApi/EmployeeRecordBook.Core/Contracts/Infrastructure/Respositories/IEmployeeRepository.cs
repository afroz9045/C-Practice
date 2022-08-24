using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;

namespace EmployeeRecordBook.Core.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task DeleteAsync(int employeeId);
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Employee> UpdateAsync(int employeeId, Employee employee);
        Task<IEnumerable<EmployeeMinimumData>> GetEmployeesByView();
    }
}