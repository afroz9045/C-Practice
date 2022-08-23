using EmployeeRecordBook.Core.Entities;
using EmployeeRecordBook.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecordBook.Infrastructure.Data
{
    public partial class EmployeeContext : DbContext
   {
      public EmployeeContext()
      {
      }

      public EmployeeContext(DbContextOptions<EmployeeContext> options)
          : base(options)
      {
      }

      public virtual DbSet<Department> Departments { get; set; } = null!;
      public virtual DbSet<Employee> Employees { get; set; } = null!;

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeRecordBook");
         }
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         modelBuilder.RegisterEntityConfigurations();

         OnModelCreatingPartial(modelBuilder);
      }

      partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
   }
}
