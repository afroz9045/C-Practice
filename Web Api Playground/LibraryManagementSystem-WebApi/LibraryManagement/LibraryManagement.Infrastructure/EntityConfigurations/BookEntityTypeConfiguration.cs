using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.EntityConfigurations
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("books");

            builder.Property(e => e.BookId).UseIdentityColumn().HasColumnName("BookId");

            builder.Property(e => e.AuthorName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("AuthorName");

            builder.Property(e => e.BookEdition)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BookEdition");

            builder.Property(e => e.BookName)
                .HasMaxLength(15)
                .HasColumnName("BookName");

            builder.Property(e => e.Isbn).HasColumnName("Isbn");

            builder.Property(e => e.StockAvailable).HasColumnName("StockAvailable");
        }
    }
}