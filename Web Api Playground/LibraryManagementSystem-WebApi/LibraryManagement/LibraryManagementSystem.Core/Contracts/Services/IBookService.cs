using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IBookService
    {
        Book? AddBookAsync(Book book, Book? existingBook);

        Book? UpdateBooksAsync(Book book, Book? existingBook);

        Book? AddInitialBookStock(Book? book);

        Book? IncrementBookStock(Book? book);
    }
}