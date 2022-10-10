using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStaffService
    {
        Staff? AddStaffAsync(Staff staff, Staff? recentStaffRecord);

        Staff UpdateStaffAsync(Staff existingStaff, Staff staffToBeUpdate);
    }
}