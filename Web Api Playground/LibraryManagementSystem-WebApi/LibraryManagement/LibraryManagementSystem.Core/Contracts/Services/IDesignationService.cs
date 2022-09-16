using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IDesignationService
    {
        Task<Designation?> AddDesignationAsync(Designation designation, Designation? existingDesignation);

        Task<string?> GenerateDesignationId();

        Designation? UpdateDesignationAsync(string designationId, Designation designation, Designation existingDesignation);
    }
}