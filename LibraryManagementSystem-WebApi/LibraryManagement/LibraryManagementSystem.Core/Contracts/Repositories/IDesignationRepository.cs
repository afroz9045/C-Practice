using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IDesignationRepository
    {
        Task<Designation?> AddDesignationAsync(Designation designation);

        Task<Designation?> GetRecentInsertedDesignation();

        Task<Designation?> DeleteDesignationAsync(Designation designation);

        Task<IEnumerable<Designation>?> GetDesignationAsync();

        Task<Designation?> GetDesignationByIdAsync(string designationId);

        Task<Designation?> GetDesignationByNameAsync(string designationName);

        Task<Designation?> UpdateDesignationAsync(Designation designation);
    }
}