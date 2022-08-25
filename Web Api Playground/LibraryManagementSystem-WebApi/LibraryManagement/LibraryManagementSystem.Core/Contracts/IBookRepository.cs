using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IBookRepository
    {
        Task<BooksDto> AddBookAsync(BooksDto bookDto);
    }
}