using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public interface IIssueService
    {
        Task<Issue?> AddBookIssueAsync(Issue issue);

        Task<Issue?> DeleteIssueAsync(short issueId);

        Task<IEnumerable<Issue>?> GetBookIssuedAsync();

        Task<Issue?> GetBookIssuedByIdAsync(short issueId);

        //Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int? studentId = 0, string? staffId = null);
        Task<Issue?> UpdateBookIssuedAsync(short issueId, Issue issue);
    }
}