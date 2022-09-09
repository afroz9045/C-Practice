using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    public class IssueEntityTypeConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.ToTable("issue");

            builder.Property(e => e.IssueId).HasColumnName("IssueId");

            builder.Property(e => e.BookId).HasColumnName("BookId");

            builder.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("ExpiryDate");

            builder.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("IssueDate");

            builder.Property(e => e.StaffId)
                .HasMaxLength(50)
                .HasColumnName("StaffId");

            builder.Property(e => e.StudentId).HasColumnName("studentId");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.Issues)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__issue__bookId__4222D4EF");

            builder.HasOne(d => d.Staff)
                .WithMany(p => p.Issues)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__issue__staffId__02084FDA");

            builder.HasOne(d => d.Student)
                .WithMany(p => p.Issues)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__issue__studentId__01142BA1");
        }
    }
}