using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class IssueEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("issue");

            builder.Property(e => e.IssueId).HasColumnName("issueId");

            builder.Property(e => e.BookId).HasColumnName("bookId");

            builder.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("expiryDate");

            builder.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("issueDate");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.Issues)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__issue__bookId__4222D4EF");
        }
    }
}