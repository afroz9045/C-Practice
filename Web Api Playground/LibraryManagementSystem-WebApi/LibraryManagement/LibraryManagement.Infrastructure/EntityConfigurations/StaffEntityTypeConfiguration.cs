using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(e => e.StaffId)
                    .HasMaxLength(50)
            .HasColumnName("staffId");

            builder.Property(e => e.DesignationId)
                .HasMaxLength(50)
                .HasColumnName("designationId");

            builder.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");

            builder.Property(e => e.StaffName)
                .HasMaxLength(20)
                .HasColumnName("staffName");

            builder.HasOne(d => d.Designation)
                .WithMany(p => p.staff)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK__staff__designati__70DDC3D8");
        }
    }
}