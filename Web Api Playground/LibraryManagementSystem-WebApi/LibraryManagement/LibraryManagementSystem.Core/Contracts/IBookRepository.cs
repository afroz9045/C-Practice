using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);

        Task<IEnumerable<dynamic>> GetBooksAsync();

        Task<Book> GetBookById(int bookId);

        Task<Book> UpdateBookAsync(BookDto book, int id);

        Task<Book> DeleteBookAsync(int id);
    }
}