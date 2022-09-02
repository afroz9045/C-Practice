using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IStaffRepository
    {
        Task<Staff> AddStaffAsync(Staff staff);

        Task<Staff> DeleteStaffAsync(string staffId);

        Task<IEnumerable<Staff>> GetStaffAsync();

        Task<Staff> GetStaffByIDAsync(string staffId);

        Task<Staff> UpdateStaffAsync(Staff staff, string staffId);
    }
}