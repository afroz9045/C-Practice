using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IDesignationRepository
    {
        Task<Designation> AddDesignationAsync(Designation designation);

        Task<Designation> DeleteDepartmentAsync(string designationId);

        Task<IEnumerable<Designation>> GetDesignationAsync();

        Task<Designation> GetDesignationByIdAsync(string designationId);

        Task<Designation> UpdateDesignationAsync(string designationId, Designation designation);
    }
}