using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class ReturnEntityTypeConfiguration : IEntityTypeConfiguration<Return>
    {
        public void Configure(EntityTypeBuilder<Return> builder)
        {
            builder.ToTable("return");

            builder.Property(e => e.ReturnId).HasColumnName("returnId");

            builder.Property(e => e.BookId).HasColumnName("bookId");

            builder.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("expiryDate");

            builder.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("issueDate");

            builder.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("returnDate");

            builder.HasOne(d => d.Book)
                .WithMany(p => p.Returns)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__return__bookId__38996AB5");
        }
    }
}