using EntityFrameworkPlayground.core.Entities;
using EntityFrameworkPlayground.Infrastructure.Data;

namespace EntityFrameworkPlayground.Infrastructure.Repositories
{
    public class DepartmentRepository
    {
        public void Create(Department department)
        {
            using (var employeeContext = new EmployeeContext())
            {
            employeeContext.Departments.Add(department);
            employeeContext.SaveChanges();    
            }
        }
    }
}
