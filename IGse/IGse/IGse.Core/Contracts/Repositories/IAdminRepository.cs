using IGse.Core.Dtos;

namespace IGse.Core.Contracts.Repositories
{
    public interface IAdminRepository
    {
        Task<IEnumerable<AdminDto>> GetAdmins();
    }
}