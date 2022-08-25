using LibraryManagement.Core.Contracts;
using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryManagementSystemDbContext _bookDbContext;

        public BookRepository(LibraryManagementSystemDbContext dbContext)
        {
            _bookDbContext = dbContext;
        }
        public async Task<BooksDto> AddBookAsync(BooksDto bookDto)
        {
            _bookDbContext.Add(bookDto);
            await _bookDbContext.SaveChangesAsync();
            return bookDto;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return  await _bookDbContext.Books.ToListAsync();
        }



    }
}
