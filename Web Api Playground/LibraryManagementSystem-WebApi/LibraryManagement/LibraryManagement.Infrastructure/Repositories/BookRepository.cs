using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;

        public BookRepository(LibraryManagementSystemDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }

        public async Task<Book> AddBookAsync(BookDto book)
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

        public async Task<IEnumerable<BookDto>> GetBooksAsync()
        {
            var bookData = from book in _libraryDbContext.Books
                           select new BookDto
                           {
                               AuthorName = book.AuthorName,
                               BookEdition = book.BookEdition,
                               BookId = book.BookId,
                               BookName = book.BookName,
                               Isbn = book.Isbn
                           };
            return await bookData.ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var bookData = await (from book in _libraryDbContext.Books
                           where book.BookId == bookId
                           select new Book()
                           {
                               AuthorName = book.AuthorName,
                               BookEdition = book.BookEdition,
                               BookId = book.BookId,
                               BookName = book.BookName,
                               Isbn = book.Isbn
                           }).FirstOrDefaultAsync();

            return bookData;
        }

        public async Task<Book> UpdateBookAsync(BookDto book,int id)
        {

            var bookToBeUpdated = await GetBookById(id);

            bookToBeUpdated.AuthorName = book.AuthorName;
            bookToBeUpdated.BookEdition = book.BookEdition;
            bookToBeUpdated.BookName = book.BookName;
            bookToBeUpdated.Isbn = book.Isbn;

            _libraryDbContext.Books.Update(bookToBeUpdated);
            await _libraryDbContext.SaveChangesAsync();
            return bookToBeUpdated;
            
        }

        public async Task<Book> DeleteBookAsync(int id)
        {
            var bookToBeUpdated = await GetBookById(id);
            _libraryDbContext.Books?.Remove(bookToBeUpdated);
            await _libraryDbContext.SaveChangesAsync();
            return bookToBeUpdated;
            
        }
    }
}
