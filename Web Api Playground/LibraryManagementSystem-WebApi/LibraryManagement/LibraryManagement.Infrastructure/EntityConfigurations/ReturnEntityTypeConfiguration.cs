using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class ReturnEntityTypeConfiguration : IEntityTypeConfiguration<Return>
    {
        public void Configure(EntityTypeBuilder<Return> builder)
        {
            builder.ToTable("return");

            builder.Property(e => e.ReturnId).HasColumnName("ReturnId");

            builder.Property(e => e.BookId).HasColumnName("BookId");

            builder.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("ExpiryDate");

            builder.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("IssueDate");

            builder.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("ReturnDate");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.Returns)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__return__bookId__38996AB5");

            builder.HasOne(d => d.Issue)
                  .WithMany()
                  .HasForeignKey(d => d.IssueId)
                  .HasConstraintName("FK__return__IssueId__4F47C5E3");
        }
    }
}