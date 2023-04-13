using IGse.Core.Contracts.Repositories;
using IGse.Core.Contracts.Services;
using IGse.Core.Dtos;
using IGse.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace IGse.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly IEvcRepository _evcRepository;

        public CustomerService(ICustomerRepository customerRepository, IConfiguration configuration, IEvcRepository evcRepository)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
            _evcRepository = evcRepository;
        }


        public async Task<Customers> AddCustomerAsync(Customers customer)
        {
               
            var addedCustomer = await _customerRepository.AddCustomer(customer);
            return addedCustomer;
        }

        public async Task<bool> AddOrUpdateWalletAmount(Customers customer,int amount)
        {
            bool isCustomerWalletUpdated;
            customer.WalletAmount += amount;
            isCustomerWalletUpdated = await _customerRepository.UpdateCustomer(customer);
            return isCustomerWalletUpdated;
        }
    }
}
