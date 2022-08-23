using EmployeeRecordBook.Core.Entities;

namespace EmployeeRecordBook.Core.Contracts.Infrastructure
{
   public interface IDepartmentRepository
   {
      Task CreateAsync(Department department);
      Task CreateRangeAsync(IEnumerable<Department> departments);
   }
}