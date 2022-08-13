using EmployeeRecordBook.Core.Dtos;
using EmployeeRecordBook.Core.Entities;

namespace EmployeeRecordBook.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task CreateRangeAsync(IEnumerable<Employee> employees);
        Task DeleteAsync(int employeeId);
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(int pageIndex, int pageSize,string sortOrder, string sortField, string filterText = null);
        Task<Employee> UpdateAsync(int employeeId, Employee employee);
    }
}