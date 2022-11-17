using Application.DataTransfer;
using Application.DataTransfer.RequestParams;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AccountService(IRepositoryManager repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountForCustomer(RequestBody requestBody, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(requestBody.customerId, trackChanges);
            if (customer is null)
                throw new CustomerNotFoundException(requestBody.customerId);
            if (requestBody.initialCredit < 0.0)
                throw new CreateAccountBadRequestException(requestBody.initialCredit);
            Account account = new Account
            {

                Balance = requestBody.initialCredit,

            };
            _repository.Account.CreateAccount(account, requestBody.customerId);
            await _repository.SaveAsync();
            var account1 = await _repository.Account.GetAccountAsync(account.AccountId, trackChanges);
            var accountToReturn = _mapper.Map<AccountDto>(account1);
            return accountToReturn;
        }

        public async Task<AccountDto> GetAccountAsync(int id, bool trackChanges)
        {
            var account = await _repository.Account.GetAccountAsync(id, trackChanges);
            if(account is null)
            {
                throw new AccountNotFoundException(id);
            }
            var accountToReturn = _mapper.Map<AccountDto>(account);
            return accountToReturn;
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int id, bool trackChanges)
        {
            var account=await _repository.Account.GetAccountAsync(id, trackChanges);
            if (account is null)
            {
                throw new AccountNotFoundException(id);
            }
            var result = _mapper.Map<UserInfoDto>(account);
            return result;
        }
    }
}
