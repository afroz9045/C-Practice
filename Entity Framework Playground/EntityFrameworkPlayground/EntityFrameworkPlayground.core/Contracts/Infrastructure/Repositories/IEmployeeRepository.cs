using EntityFrameworkPlayground.core.Entities;

namespace EntityFrameworkPlayground.core.Infrastructure.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        void DeleteAsync(int employeeID);
        Task<Employee> GetEmployeeAsync(int employeeId);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> UpdateAsync(int employeeId, Employee employee);
    }
}