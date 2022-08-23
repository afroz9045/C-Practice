using EmployeeRecordBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeRecordBook.Infrastructure.EntityConfigurations
{
   internal class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
   {
      public void Configure(EntityTypeBuilder<Department> builder)
      {
         builder.ToTable("Department");

         builder.Property(e => e.Id).ValueGeneratedNever();

         builder.Property(e => e.Name).HasMaxLength(20);
      }
   }
}
