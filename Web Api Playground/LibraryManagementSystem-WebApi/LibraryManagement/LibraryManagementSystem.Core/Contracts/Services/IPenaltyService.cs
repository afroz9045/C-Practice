using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IPenaltyService
    {
        Penalty? IsPenalty(short issueId, Penalty? existingPenalty, Issue? bookIssueDetails);

        Penalty? PayPenalty(short issueId, int penaltyAmount, Penalty? existingPenalty, Issue? bookIssueDetails);
    }
}