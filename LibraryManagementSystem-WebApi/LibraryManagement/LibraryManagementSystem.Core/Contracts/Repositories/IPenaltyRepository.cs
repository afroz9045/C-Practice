using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IPenaltyRepository
    {
        Task<Penalty?> IsPenalty(Penalty isPenalty);

        Task<IEnumerable<Penalty>?> GetPenaltiesAsync();

        Task<Penalty?> GetPenaltyByIdAsync(short? issueId);

        Task<Penalty?> PayPenaltyAsync(Penalty penalty);

        Task<Penalty?> DeletePenaltyAsync(Penalty penalty);
    }
}