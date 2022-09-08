using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IPenaltyRepository
    {
        Task<Penalty?> IsPenalty(short issueId);

        Task<IEnumerable<Penalty>> GetPenaltiesAsync();

        Task<Penalty> GetPenaltyByIdAsync(short issueId);

        Task<bool> PayPenaltyAsync(short issueId, int penaltyAmount);

        Task<Penalty?> DeletePenaltyAsync(short issueId);
    }
}