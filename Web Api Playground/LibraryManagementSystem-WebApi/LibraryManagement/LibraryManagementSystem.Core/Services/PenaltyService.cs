using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IIssueRepository _issueRepository;
        private readonly IIssueService _issueService;

        public PenaltyService(IPenaltyRepository penaltyRepository, IIssueRepository issueRepository, IIssueService issueService)
        {
            _penaltyRepository = penaltyRepository;
            _issueRepository = issueRepository;
            _issueService = issueService;
        }

        public Penalty? IsPenalty(short issueId, Penalty? existingPenalty, Issue? bookIssueDetails)
        {
            //var isPenaltyAlreadyExits = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            if (existingPenalty == null && bookIssueDetails != null)
            {
                //var issueDetails = await _issueRepository.GetBookIssuedByIdAsync(issueId);
                TimeSpan expiredDate = DateTime.UtcNow.Subtract(bookIssueDetails.ExpiryDate);
                if (bookIssueDetails != null && expiredDate.Days > 0)
                {
                    var penalty = new Penalty()
                    {
                        IssueId = bookIssueDetails.IssueId,
                        PenaltyAmount = expiredDate.Days * 2,
                        PenaltyPaidStatus = false
                    };
                    //var penaltyResult = await _penaltyRepository.IsPenalty(penalty);
                    //if (penaltyResult != null)
                    return penalty;
                }
            }
            else if (existingPenalty != null)
            {
                return existingPenalty;
            }

            return null;
        }

        public Penalty? PayPenalty(short issueId, int penaltyAmount, Penalty? existingPenalty, Issue? bookIssueDetails)
        {
            //var penalty = IsPenalty(issueId, existingPenalty, bookIssueDetails);
            if (existingPenalty != null && penaltyAmount == existingPenalty.PenaltyAmount)
            {
                existingPenalty.PenaltyPaidStatus = true;
                return existingPenalty;
            }
            return existingPenalty;
        }
    }
}