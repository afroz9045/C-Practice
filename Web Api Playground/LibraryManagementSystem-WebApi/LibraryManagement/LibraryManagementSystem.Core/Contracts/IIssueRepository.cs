using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IIssueRepository
    {
        Task<Issue> AddBookIssueAsync(Issue issue);

        Task<IEnumerable<Issue>> GetBookIssuedAsync();

        Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int? studentId = 0, string? staffId = null);

        Task<Issue> GetBookIssuedByIdAsync(short issueId);

        Task<Issue> UpdateBookIssuedAsync(short issueId, Issue issue);

        Task<Issue> DeleteIssueAsync(short IssueId);
    }
}