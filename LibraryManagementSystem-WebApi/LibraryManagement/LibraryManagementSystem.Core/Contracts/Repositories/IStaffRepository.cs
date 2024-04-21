using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff> AddStaffAsync(Staff staff);

        Task<Staff> DeleteStaffAsync(Staff staff);

        Task<IEnumerable<Staff>> GetStaffAsync();

        Task<Staff?> GetRecentInsertedStaff();

        Task<Staff?> GetStaffByIdAsync(string? staffId);

        Task<Staff?> GetStaffByName(string staffName);

        Task<Staff> UpdateStaffAsync(Staff staff);
    }
}