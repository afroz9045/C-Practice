using EmployeeRecordBook.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRecordBook.Infrastructure.Extensions
{
   internal static class ModelBuilderExtensions
   {
      internal static void RegisterEntityConfigurations(this ModelBuilder modelBuilder)
      {
         modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
         modelBuilder.ApplyConfiguration(new DepartmentEntityTypeConfiguration());
      }
   }
}
