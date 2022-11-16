using Application.DataTransfer.RequestParams;
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

        public async Task<Account> CreateAccountForCustomer(RequestBody requestBody, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(requestBody.customerId, trackChanges);
            Account account = new Account
            {
                Customer = customer,
                CustomerId = customer.CustomerId,
                Balance = requestBody.initialCredit,

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
