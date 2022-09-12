using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IReturnService
    {
        Task<Return?> AddReturnAsync(Return returnDetails, short issueId);
        Task<Return?> DeleteReturnAsync(int returnId);
        Task<IEnumerable<Return>?> GetReturnAsync();
        Task<Return?> GetReturnByIdAsync(int returnId);
        Task<Return?> UpdateReturnAsync(int returnId, Return returnDetails);
    }
}