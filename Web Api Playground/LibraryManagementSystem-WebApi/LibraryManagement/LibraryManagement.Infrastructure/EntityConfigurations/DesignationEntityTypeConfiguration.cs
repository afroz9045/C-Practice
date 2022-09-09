using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class DesignationEntityTypeConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.ToTable("designation");

            builder.Property(e => e.DesignationId)
                .HasMaxLength(50)
                .HasColumnName("DesignationId");

            builder.Property(e => e.DesignationName)
                .HasMaxLength(20)
                .HasColumnName("DesignationName");
        }
    }
}