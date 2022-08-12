using EmployeeRecordBook.Core.Contracts.Infrastructure;
using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRecordBook.Infrastructure.Repositories
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
