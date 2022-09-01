using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class DesignationEntityTypeConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.ToTable("designation");

            builder.Property(e => e.DesignationId)
                .HasMaxLength(50)
                .HasColumnName("designationId");

            builder.Property(e => e.DesignationName)
                .HasMaxLength(20)
                .HasColumnName("designation");

            builder.Property(e => e.StaffId)
                .HasMaxLength(50)
            .HasColumnName("staffId");

            builder.HasOne(d => d.Staff)
                .WithMany(p => p.Designations)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__designati__staff__46E78A0C");
        }
    }
}