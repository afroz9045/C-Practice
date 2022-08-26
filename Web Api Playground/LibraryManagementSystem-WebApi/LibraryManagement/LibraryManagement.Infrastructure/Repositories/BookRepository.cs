using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository:IBookRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;

        public BookRepository(LibraryManagementSystemDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Book> AddBook(BookDto book)
        {
            var bookDetails = new Book()
            {
                AuthorName = book.AuthorName,
                BookEdition = book.BookEdition,
                BookId = book.BookId,
                BookName = book.BookName,
                Isbn = book.Isbn

            };
            _libraryDbContext.Books.Add(bookDetails);
            await _libraryDbContext.SaveChangesAsync();
            return bookDetails;
        }
    }
}
