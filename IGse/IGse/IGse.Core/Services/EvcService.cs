using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Entities;
using System.Data;

namespace IGse.Core.Services
{
    public class EvcService : IEvcService
    {
        private readonly IEvcRepository _evcRepository;
        private readonly ICustomerRepository _customerRepository;

        public EvcService(IEvcRepository evcRepository, ICustomerRepository customerRepository)
        {
            _evcRepository = evcRepository;
            _customerRepository = customerRepository;
        }


        public async Task<string> AddEvcAsync()
        {
            Evc subsidyVoucher = new Evc()
            {
                EvcVoucher = Guid.NewGuid().ToString().Substring(0, 8),
                IsUsed = false,
                Amount = 200,
            };
            var addedEvc = await _evcRepository.AddEvcAsync(subsidyVoucher);
            return addedEvc.EvcVoucher;
        }

        public async Task<String> AddCustomEvc(int amount)
        {
            Evc customEvc = new Evc()
            {
                EvcVoucher = Guid.NewGuid().ToString().Substring(0, 8),
                IsUsed = false,
                Amount = amount
            };
            var addedEvc = await _evcRepository.AddEvcAsync(customEvc);
            return addedEvc.EvcVoucher;
        }

        public async Task<bool> ValidateEvc(string evcVoucher)
        {
            var evcs = await _evcRepository.GetEvcsAsync();

            var isValidEvc = evcs.Any(voucher => voucher.EvcVoucher == evcVoucher && !voucher.IsUsed);
            return isValidEvc;
        }

        public async Task<Evc> MarkEvcAsUsed(int customerId, Evc evc)
        {
            evc.IsUsed = true;
            evc.UsedByCustomer = customerId;

            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer != null)
            {
                customer.WalletAmount += evc.Amount;
                await _customerRepository.UpdateCustomer(customer);
            }
                var updatedEvc = await _evcRepository.UpdateEvcAsync(evc);
            return updatedEvc;
        }

    }
}
