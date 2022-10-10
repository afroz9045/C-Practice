using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> AddBookAsync(Book? book);

        Task<IEnumerable<Book>?> GetBooksAsync();

        Task<Book?> GetBookById(int? bookId);

        Task<Book?> GetBookByBookName(string bookName);

        Task<IEnumerable<Book>?> GetOutOfStockBooks();

        Task<Book> UpdateBookAsync(Book book);

        Task<Book?> DeleteBookAsync(Book book);
    }
}