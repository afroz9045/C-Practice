using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IBookRepository
    {
        Task<Book> AddBook(BookDto book);
    }
}
