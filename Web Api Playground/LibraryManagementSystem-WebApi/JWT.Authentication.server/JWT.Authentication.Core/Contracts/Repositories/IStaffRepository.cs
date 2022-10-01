using JWT.Authentication.Core.Entities;

namespace JWT.Authentication.Server.Core.Contract.Repositories
{
    public interface IStaffRepository
    {
        Task<staff?> GetStaffByStaffId(string staffId);
    }
}