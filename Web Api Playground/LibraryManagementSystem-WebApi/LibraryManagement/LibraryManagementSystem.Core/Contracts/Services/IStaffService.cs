using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Services
{
    public interface IStaffService
    {
        Task<Staff?> AddStaffAsync(Staff staff);
        Task<Staff?> DeleteStaffAsync(string staffId);
        Task<IEnumerable<Staff>?> GetStaffAsync();
        Task<Staff?> GetStaffByIdAsync(string staffId);
        Task<Staff?> UpdateStaffAsync(Staff staff, string staffId);
    }
}