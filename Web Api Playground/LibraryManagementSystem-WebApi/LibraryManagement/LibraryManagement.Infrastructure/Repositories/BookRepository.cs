using AutoMapper;
using Dapper;
using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using System.Data;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        public BookRepository()
        {
        }

        private readonly LibraryManagementSystemDbContext _libraryDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IMapper _mapper;

        public BookRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _mapper = mapper;
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

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var gettingBooksQuery = "select * from [books]";
            var bookData = await _dapperConnection.QueryAsync<Book>(gettingBooksQuery);
            return bookData;
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var getBookByIdQuery = "select * from [Books] where bookId=@bookId";
            return await _dapperConnection.QueryFirstAsync<Book>(getBookByIdQuery, new { bookId = bookId });
        }

        public async Task<Book> UpdateBookAsync(Book book, int id)
        {
            var bookToBeUpdated = await GetBookById(id);
            bookToBeUpdated.BookId = book.BookId;
            bookToBeUpdated.AuthorName = book.AuthorName;
            bookToBeUpdated.BookEdition = book.BookEdition;
            bookToBeUpdated.BookName = book.BookName;
            bookToBeUpdated.Isbn = book.Isbn;

            _libraryDbContext.Update(bookToBeUpdated);
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