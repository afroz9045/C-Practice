using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;

        public BookRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            if (_libraryDbContext.Books.Count() == 0)
            {
                var identityResetQuery = "DBCC CHECKIDENT ('[books]',RESEED,0)";
                await _dapperConnection.QueryAsync(identityResetQuery);
            }
            var bookDetails = await GetBookById(book.BookId);
            if (bookDetails == null)
            {
                var bookRecord = new Book()
                {
                    AuthorName = book.AuthorName,
                    BookEdition = book.BookEdition,
                    BookId = book.BookId,
                    BookName = book.BookName,
                    Isbn = book.Isbn,
                    StockAvailable = 1,
                };
                _libraryDbContext.Books.Add(bookRecord);
                await _libraryDbContext.SaveChangesAsync();
                return bookRecord;
            }
            else
            {
                bookDetails.StockAvailable += 1;
                _libraryDbContext.Books.Update(bookDetails);
                await _libraryDbContext.SaveChangesAsync();
                return bookDetails;
            }
        }

        public async Task<IEnumerable<dynamic>> GetBooksAsync()
        {
            //var bookData = await (from book in _libraryDbContext.Books
            //                      select new BookDto()
            //                      {
            //                          AuthorName = book.AuthorName,
            //                          BookEdition = book.BookEdition,
            //                          BookId = book.BookId,
            //                          BookName = book.BookName,
            //                          Isbn = book.Isbn,
            //                          StockAvailable = book.StockAvailable
            //                      }).ToListAsync();
            var gettingBooksQuery = "select * from [books]";
            var bookData = await _dapperConnection.QueryAsync(gettingBooksQuery);
            return bookData;
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
                                      Isbn = book.Isbn,
                                      StockAvailable = book.StockAvailable
                                  }).FirstOrDefaultAsync();

            return bookData;
        }

        public async Task<Book> UpdateBookAsync(BookDto book, int id)
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