using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Contracts
{
    public interface IStaffRepository
    {
        Task<Staff> AddStaffAsync(Staff staff);

        Task<Staff> DeleteStaffAsync(Guid staffId);

        Task<IEnumerable<Staff>> GetStaffAsync();

        Task<Staff> GetStaffByIDAsync(Guid staffId);

        Task<Staff> UpdateStaffAsync(Staff staff, Guid staffId);
    }
}