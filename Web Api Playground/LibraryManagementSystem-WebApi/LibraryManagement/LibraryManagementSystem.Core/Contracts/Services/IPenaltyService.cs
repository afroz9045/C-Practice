using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IPenaltyService
    {
        Task<Penalty?> DeletePenaltyAsync(short issueId);
        Task<IEnumerable<Penalty>?> GetPenaltiesAsync();
        Task<Penalty?> GetPenaltyByIdAsync(short issueId);
        Task<Penalty?> IsPenalty(short issueId);
        Task<bool> PayPenaltyAsync(short issueId, int penaltyAmount);
    }
}