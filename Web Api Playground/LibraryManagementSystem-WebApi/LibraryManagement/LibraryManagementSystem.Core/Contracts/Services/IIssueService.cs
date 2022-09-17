using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public interface IIssueService
    {
        Issue? AddBookIssueAsync(Issue issue, Book? bookIdResult, Staff? staffIdValidate, Student? studentIdValidate);

        Issue? UpdateBookIssuedAsync(short issueId, Issue existingIssue, Issue issue);
    }
}