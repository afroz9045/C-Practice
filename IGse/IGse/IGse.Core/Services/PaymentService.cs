using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;

namespace IGse.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBillRepository _billRepository;

        public PaymentService(IPaymentRepository paymentRepository, IBillRepository billRepository)
        {
            _paymentRepository = paymentRepository;
            _billRepository = billRepository;
        }

        public async Task<Payments> PayBillAsync(Bill bill, Customers customer)
        {
            customer.WalletAmount -= bill.Amount;
            bill.IsPaid = true;
            Payments payment = new Payments()
            {
                BillId = bill.BillId,
                CustomerId = customer.CustomerId,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = true
            };
            var paymentRecord = await _paymentRepository.PayBill(customer, bill, payment);
            return paymentRecord;
        }
    }
}
