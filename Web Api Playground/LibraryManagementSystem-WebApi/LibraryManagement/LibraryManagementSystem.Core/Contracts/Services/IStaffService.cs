using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStaffService
    {
        Task<Staff?> AddStaffAsync(Staff staff);

        Staff UpdateStaffAsync(Staff existingStaff, Staff staffToBeUpdate);
    }
}