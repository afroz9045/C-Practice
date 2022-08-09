using EntityFrameworkPlayground.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPlayground.Infrastructure.Data
{
    public class EmployeeContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localDb)\MSSQLLocalDB;Database = EmployeeDB;Trusted_Connection = True;");
        }
    }
}
