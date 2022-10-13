using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IReturnRepository
    {
        Task<Return?> AddReturnAsync(Return returnDetails, Book book, Issue bookIssue);

        Task<Return> DeleteReturnAsync(Return bookReturn);

        Task<IEnumerable<Return?>> GetReturnAsync();

        Task<IEnumerable<PendingBookReturnDto>> GetPendingBookToBeReturn();

        Task<Return?> GetReturnByIdAsync(int returnId);

        Task<IEnumerable<Return>> GetBooksReturnedByDateRange(DateTime fromDate, DateTime? toDate = null);

        Task<Return> UpdateReturnAsync(Return returnDetails);
    }
}