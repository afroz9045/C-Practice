using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IDesignationService
    {
        Task<Designation?> AddDesignationAsync(Designation designation);
        Task<Designation?> DeleteDesignationAsync(string designationId);
        Task<string?> GenerateDesignationId();
        Task<IEnumerable<Designation>?> GetDesignationAsync();
        Task<Designation?> GetDesignationByIdAsync(string designationId);
        Task<Designation?> GetDesignationByNameAsync(string designationName);
        Task<Designation?> UpdateDesignationAsync(string designationId, Designation designation);
    }
}