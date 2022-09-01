using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IDesignationRepository
    {
        Task<Designation> AddDesignationAsync(Designation designation);

        Task<Designation> DeleteDepartmentAsync(Guid designationId);

        Task<IEnumerable<Designation>> GetDesignationAsync();

        Task<Designation> GetDesignationByIdAsync(Guid designationId);

        Task<Designation> UpdateDesignationAsync(Guid designationId, Designation designation);
    }
}