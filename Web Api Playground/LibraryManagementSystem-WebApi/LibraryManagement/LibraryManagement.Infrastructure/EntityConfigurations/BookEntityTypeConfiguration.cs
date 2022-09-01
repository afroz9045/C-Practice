using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Dapper.SqlMapper;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");

            builder.Property(e => e.BookId).HasColumnName("bookId");

            builder.Property(e => e.AuthorName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("authorName");

            builder.Property(e => e.BookEdition)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("bookEdition");

            builder.Property(e => e.BookName)
                .HasMaxLength(15)
            .HasColumnName("bookName");

            builder.Property(e => e.Isbn).HasColumnName("isbn");

            builder.Property(e => e.StockAvailable).HasColumnName("stockAvailable");
        }
    }
}