using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class DepartmentEntityTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.DeptId)
                    .HasName("PK__departme__BE2D26B6D0D236DC");

            builder.ToTable("department");

            builder.Property(e => e.DeptId).HasColumnName("DeptId");

            builder.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .HasColumnName("DepartmentName");
        }
    }
}