using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IBookRepository
    {
        Task<Book> AddBookAsync(Book book);

        Task<IEnumerable<Book>> GetBooksAsync();

        Task<Book?> GetBookById(int bookId);

        Task<Book> UpdateBookAsync(Book book, int id);

        Task<Book> DeleteBookAsync(int id);
    }
}