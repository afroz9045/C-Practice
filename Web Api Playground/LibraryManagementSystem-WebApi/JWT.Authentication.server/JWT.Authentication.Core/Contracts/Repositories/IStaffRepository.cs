using JWT.Authentication.Core.Entities;

namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface IStaffRepository
    {
        Task<Staff?> GetStaffByStaffId(string staffId);
    }
}