using AutoMapper;
using Dapper;
using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
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
        private readonly IMapper _mapper;

        public BookRepository(LibraryManagementSystemDbContext libraryDbContext, IDbConnection dapperConnection, IMapper mapper)
        {
            _libraryDbContext = libraryDbContext;
            _dapperConnection = dapperConnection;
            _mapper = mapper;
        }

        public async Task<Book?> AddBookAsync(Book? book)
        {
            if (_libraryDbContext.Books.Count() == 0)
            {
                var identityResetQuery = "DBCC CHECKIDENT ('[books]',RESEED,0)";
                await _dapperConnection.QueryAsync<Book>(identityResetQuery);
            }
            if (book != null && book.StockAvailable == 1)
            {
                _libraryDbContext.Books.Add(book);
                await _libraryDbContext.SaveChangesAsync();
                return book;
            }
            else if (book != null && book.StockAvailable > 1)
            {
                _libraryDbContext.Books.Update(book);
                await _libraryDbContext.SaveChangesAsync();
                return book;
            }
            return null;
        }

        public async Task<Book?> GetBookByBookName(string bookName)
        {
            var bookRecord = await (from book in _libraryDbContext.Books
                                    where book.BookName == bookName
                                    select book).FirstOrDefaultAsync();
            if (bookRecord != null)
                return bookRecord;
            return null;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var gettingBooksQuery = "select * from [books]";
            var bookData = await _dapperConnection.QueryAsync<Book>(gettingBooksQuery);
            return bookData;
        }

        public async Task<Book?> GetBookById(int bookId)
        {
            var getBookByIdQuery = "select * from [Books] where BookId=@bookId";
            var bookResult = await _dapperConnection.QueryFirstAsync<Book>(getBookByIdQuery, new { bookId });
            return bookResult;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            _libraryDbContext.Update(book);
            await _libraryDbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteBookAsync(Book book)
        {
            if (book != null)
            {
                _libraryDbContext.Books?.Remove(book);
                await _libraryDbContext.SaveChangesAsync();
                return book;
            }
            return null;
        }
    }
}