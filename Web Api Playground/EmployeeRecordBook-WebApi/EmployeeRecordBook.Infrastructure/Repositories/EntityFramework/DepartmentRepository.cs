using EmployeeRecordBook.Core.Contracts.Infrastructure;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Infrastructure.Data;

namespace EmployeeRecordBook.Infrastructure.Repositories.EntityFramework
{
    public class DepartmentRepository : IDepartmentRepository
   {
      
      public async Task CreateAsync(Department department)
      {
         // Not ideal way to use DB Context instance here, instead use constuctor injection. 
         using (var employeeContext = new EmployeeContext())
         {
            employeeContext.Departments.Add(department);
            await employeeContext.SaveChangesAsync();
         }
      }
      public async Task CreateRangeAsync(IEnumerable<Department> departments)
      {
         // Not ideal way to use DB Context instance here, instead use constuctor injection. 
         using (var employeeContext = new EmployeeContext())
         {
            employeeContext.Departments.AddRange(departments);
            await employeeContext.SaveChangesAsync();
         }
      }
   }
}
