using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IReturnService
    {
        (Return?, Book?) AddReturn(Return returnDetails, short issueId, Penalty? isPenalty, Book? bookDetails, Issue issueDetails);

        Return? UpdateReturnAsync(int returnId, Return? existingReturnDetails, Return returnDetailsToBeUpdate);
    }
}