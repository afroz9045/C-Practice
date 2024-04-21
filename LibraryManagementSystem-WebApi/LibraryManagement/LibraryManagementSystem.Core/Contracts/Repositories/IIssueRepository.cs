using LibraryManagement.Core.Dtos;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IIssueRepository
    {
        Task<Issue?> AddBookIssueAsync(Issue? issue, Book? book);

        int GetBooksToBeReturnByEntity(int? studentId, string? staffId);

        Task<IEnumerable<Issue>?> GetBookIssuedAsync();

        Task<IEnumerable<BookIssuedTo>?> GetBookIssuedToEntityDetails(int studentId, string? staffId = null);

        Task<Issue?> GetBookIssuedByIdAsync(short issueId);

        Task<IEnumerable<Issue>> GetBooksIssuedByDateRange(DateTime fromDate, DateTime? toDate = null);

        Task<IEnumerable<Issue>?> GetBookIssuedByBookId(int bookId);

        Task<Issue?> UpdateBookIssuedAsync(Issue? issue);

        Task<Issue?> DeleteIssueAsync(Issue issuedRecord);
    }
}