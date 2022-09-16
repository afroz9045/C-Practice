using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IIssueRepository
    {
        Task<Issue?> AddBookIssueAsync(Issue issue, Book book);

        Task<IEnumerable<Issue>?> GetBookIssuedAsync();

        Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int studentId, string? staffId = null);

        Task<Issue?> GetBookIssuedByIdAsync(short issueId);

        Task<Issue?> UpdateBookIssuedAsync(Issue issue);

        Task<Issue> DeleteIssueAsync(Issue issuedRecord);
    }
}