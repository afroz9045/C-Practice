using IGse.Core.Entities;

namespace IGse.Core.Contracts.Services
{
    public interface IPaymentService
    {
        Task<Payments> PayBillAsync(Bill bill, Customers customer);
    }
}