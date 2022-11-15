using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repository;

        public AccountService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Account> CreateAccountForCustomer(int customerId, double initialCredit, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(customerId, trackChanges);
            var account = new Account
            {
                Customer = customer,
                CustomerId = customerId,
                Balance = initialCredit,
            };
            return account;
        }

        public async Task<Account> GetAccountAsync(int id, bool trackChanges)
        {
            var account = await _repository.Account.GetAccountAsync(id, trackChanges);
            return account;
        }
    }
}
