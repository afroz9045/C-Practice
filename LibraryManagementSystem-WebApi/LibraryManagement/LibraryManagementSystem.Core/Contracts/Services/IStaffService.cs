using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStaffService
    {
        Staff? AddStaff(Staff staff, Staff? recentStaffRecord);

        Staff UpdateStaff(Staff existingStaff, Staff staffToBeUpdate);
    }
}