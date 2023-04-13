using IGse.Core.Contracts.Repositories;
using IGse.Core.Entities;
using IGse.Infrastructure.Data;

namespace IGse.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly GseDbContext _gseDbContext;

        public PaymentRepository(GseDbContext gseDbContext)
        {
            _gseDbContext = gseDbContext;
        }


        public async Task<Payments> PayBill(Customers customer, Bill bill, Payments payment)
        {
            _gseDbContext.Payments.Add(payment);
            _gseDbContext.Customers.Update(customer);
            _gseDbContext.Bills.Update(bill);
            await _gseDbContext.SaveChangesAsync();
            return payment;
        }

    }
}
