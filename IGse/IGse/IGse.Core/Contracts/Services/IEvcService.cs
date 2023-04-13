using IGse.Core.Entities;

namespace IGse.Core.Contracts.Services
{
    public interface IEvcService
    {
        Task<string> AddEvcAsync();
        Task<String> AddCustomEvc(int amount);
        Task<bool> ValidateEvc(string evcVoucher);
        Task<Evc> MarkEvcAsUsed(int customerId, Evc evc);
    }
}