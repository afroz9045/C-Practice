using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IReturnRepository
    {
        Task<Return?> AddReturnAsync(Return returnDetails, Book book);

        Task<Return> DeleteReturnAsync(Return bookReturn);

        Task<IEnumerable<Return>> GetReturnAsync();

        Task<Return> GetReturnByIdAsync(int returnId);

        Task<Return> UpdateReturnAsync(Return returnDetails);
    }
}