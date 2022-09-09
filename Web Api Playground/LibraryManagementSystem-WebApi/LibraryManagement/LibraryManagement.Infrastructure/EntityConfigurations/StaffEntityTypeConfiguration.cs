using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class StaffEntityTypeConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(e => e.StaffId)
                     .HasMaxLength(50)
                     .HasColumnName("StaffId");

            builder.Property(e => e.DesignationId)
                .HasMaxLength(50)
                .HasColumnName("DesignationId");

            builder.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("Gender");

            builder.Property(e => e.StaffName)
                .HasMaxLength(20)
                .HasColumnName("StaffName");

            builder.HasOne(d => d.Designation)
                .WithMany(p => p.staff)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK__staff__designati__70DDC3D8");
        }
    }
}