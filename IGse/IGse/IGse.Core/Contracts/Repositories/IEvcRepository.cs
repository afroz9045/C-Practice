using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface IEvcRepository
    {
        Task<Evc> AddEvcAsync(Evc evc);
        Task<bool> DeleteEvcAsync(Evc evc);
        Task<Evc> GetEvcByIdAsync(int evcId);
        Task<Evc> GetEvcByVoucher(string voucherCode);
        Task<Evc?> GetSubsidyEvc();
        Task<IEnumerable<Evc>> GetEvcsAsync();
        Task<Evc> UpdateEvcAsync(Evc evc);
    }
}