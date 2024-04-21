using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IDesignationService
    {
        Designation? AddDesignationAsync(Designation designation, Designation? recentDesignation);

        Designation? UpdateDesignationAsync(string designationId, Designation designation, Designation existingDesignation);
    }
}