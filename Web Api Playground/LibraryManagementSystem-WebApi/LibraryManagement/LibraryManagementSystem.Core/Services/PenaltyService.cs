using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class PenaltyService : IPenaltyService
    {
        public Penalty? IsPenalty(short issueId, Penalty? existingPenalty, Issue? bookIssueDetails)
        {
            if (existingPenalty == null && bookIssueDetails != null)
            {
                TimeSpan expiredDate = DateTime.UtcNow.Subtract(bookIssueDetails.ExpiryDate);
                if (bookIssueDetails != null && expiredDate.Days > 0)
                {
                    var penalty = new Penalty()
                    {
                        IssueId = bookIssueDetails.IssueId,
                        PenaltyAmount = expiredDate.Days * 2,
                        PenaltyPaidStatus = false
                    };
                    return penalty;
                }
            }
            else if (existingPenalty != null)
            {
                return existingPenalty;
            }

            return null;
        }

        public Penalty? PayPenalty(int penaltyAmount, Penalty? existingPenalty)
        {
            if (existingPenalty != null && penaltyAmount == existingPenalty.PenaltyAmount)
            {
                existingPenalty.PenaltyPaidStatus = true;
                return existingPenalty;
            }
            return existingPenalty;
        }
    }
}