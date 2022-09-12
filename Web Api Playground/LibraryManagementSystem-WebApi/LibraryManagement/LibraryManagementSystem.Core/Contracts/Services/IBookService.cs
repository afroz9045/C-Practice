using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IBookService
    {
        Task<Book?> AddBookAsync(Book book);

        Task<IEnumerable<Book>?> GetBooksAsync();

        Task<Book?> GetBookByNameAsync(string bookName);

        Task<Book?> GetBookByBookId(int bookId);

        Task<Book?> UpdateBooksAsync(Book book, int bookId);

        Task<Book?> DeleteBookAsync(int bookId);

        Book? AddInitialBookStock(Book? book);

        Book? IncrementBookStock(Book? book);
    }
}