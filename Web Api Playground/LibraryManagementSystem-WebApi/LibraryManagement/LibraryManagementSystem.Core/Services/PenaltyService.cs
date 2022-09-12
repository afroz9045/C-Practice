using LibraryManagement.Core.Contracts.Repositories;
using LibraryManagement.Core.Contracts.Services;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Services
{
    public class PenaltyService : IPenaltyService
    {
        private readonly IPenaltyRepository _penaltyRepository;
        private readonly IIssueService _issueService;

        public PenaltyService(IPenaltyRepository penaltyRepository, IIssueService issueService)
        {
            _penaltyRepository = penaltyRepository;
            _issueService = issueService;
        }

        public async Task<IEnumerable<Penalty>?> GetPenaltiesAsync()
        {
            var penalties = await _penaltyRepository.GetPenaltiesAsync();
            if (penalties != null)
                return penalties;
            return null;
        }

        public async Task<Penalty?> GetPenaltyByIdAsync(short issueId)
        {
            var penalty = await _penaltyRepository.GetPenaltyByIdAsync(issueId);
            if (penalty != null)
                return penalty;
            return null;
        }

        public async Task<Penalty?> IsPenalty(short issueId)
        {
            var isPenaltyAlreadyExits = await GetPenaltyByIdAsync(issueId);
            if (isPenaltyAlreadyExits == null)
            {
                var issueDetails = await _issueService.GetBookIssuedByIdAsync(issueId);
                TimeSpan expiredDate = DateTime.UtcNow.Subtract(issueDetails.ExpiryDate);
                if (issueDetails != null && expiredDate.Days > 0)
                {
                    var penalty = new Penalty()
                    {
                        IssueId = issueDetails.IssueId,
                        PenaltyAmount = expiredDate.Days * 2,
                        PenaltyPaidStatus = false
                    };
                    var penaltyResult = await _penaltyRepository.IsPenalty(penalty);
                    if (penaltyResult != null)
                        return penaltyResult;
                }
            }
            else if (isPenaltyAlreadyExits != null)
            {
                return isPenaltyAlreadyExits;
            }

            return null;
        }

        public async Task<bool> PayPenaltyAsync(short issueId, int penaltyAmount)
        {
            var penalty = await IsPenalty(issueId);
            if (penalty != null && penaltyAmount == penalty.PenaltyAmount)
            {
                //penalty.PenaltyAmount -= penaltyAmount;
                penalty.PenaltyPaidStatus = true;
                var isPenaltyPaid = await _penaltyRepository.PayPenaltyAsync(penalty);
                return isPenaltyPaid;
            }
            return false;
        }

        public async Task<Penalty?> DeletePenaltyAsync(short issueId)
        {
            var penalty = await GetPenaltyByIdAsync(issueId);
            if (penalty != null)
            {
                var deletedPenalty = await _penaltyRepository.DeletePenaltyAsync(penalty);
                return deletedPenalty;
            }
            return null;
        }
    }
}