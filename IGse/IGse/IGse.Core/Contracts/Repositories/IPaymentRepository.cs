using IGse.Core.Entities;

namespace IGse.Core.Contracts.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payments> PayBill(Customers customer, Bill bill, Payments payment);
    }
}