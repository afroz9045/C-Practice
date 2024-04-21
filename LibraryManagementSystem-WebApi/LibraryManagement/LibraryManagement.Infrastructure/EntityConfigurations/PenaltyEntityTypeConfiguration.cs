using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class PenaltyEntityTypeConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.ToTable("penalty");

            builder.Property(e => e.PenaltyId).HasColumnName("PenaltyId");

            builder.Property(e => e.IssueId).HasColumnName("IssueId");

            builder.Property(e => e.PenaltyAmount)
                .HasColumnName("PenaltyAmount")
                .HasDefaultValueSql("((0))");

            builder.Property(e => e.PenaltyPaidStatus)
                .HasColumnName("PenaltyPaidStatus")
                .HasDefaultValueSql("((0))");

            builder.HasOne(d => d.Issue)
                .WithMany(p => p.Penalties)
                .HasForeignKey(d => d.IssueId)
                .HasConstraintName("FK__penalty__issueId__04E4BC85");
        }
    }
}