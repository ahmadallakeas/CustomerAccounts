using Application.DataTransfer;
using Application.DataTransfer.RequestParams;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Serilog;
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
        public AccountService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountForCustomer(int customerId, double initialCredits, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(customerId, trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException(customerId);

            }
            if (initialCredits < 0.0)
            {
                Log.Error($"Error in input data, initial credit less than 0");
                throw new CreateAccountBadRequestException(initialCredits);
            }
            Account account = new Account
            {

                Balance = initialCredits,

            };
            _repository.Account.CreateAccount(account, customerId);
            await _repository.SaveAsync();
            var account1 = await _repository.Account.GetAccountAsync(account.AccountId, trackChanges);
            var accountToReturn = _mapper.Map<AccountDto>(account1);
            return accountToReturn;
        }

        public async Task<AccountDto> GetAccountAsync(int id, bool trackChanges)
        {
            var account = await _repository.Account.GetAccountAsync(id, trackChanges);
            if (account is null)
            {
                Log.Error($"Account with id {id} does not exist");
                throw new AccountNotFoundException(id);
            }
            var accountToReturn = _mapper.Map<AccountDto>(account);
            return accountToReturn;
        }

        public async Task<AccountDto> GetAccountForCustomerAsync(int customerId, int accountId, bool trackChanges)
        {
            var account = await _repository.Account.GetAccountByCustomerIdAsync(customerId, accountId, trackChanges);
            if (account is null)
            {
                Log.Error($"Account with id {accountId} does not exist");
                throw new AccountNotFoundException(customerId, accountId);
            }
            var accountToReturn = _mapper.Map<AccountDto>(account);
            return accountToReturn;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsAsync(int customerId, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(customerId, trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException(customerId);
            }
            var accounts = await _repository.Account.GetAccountsAsync(customerId, trackChanges);
            var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return accountsDto;
        }

        public async Task<UserInfoDto> GetUserInfoAsync(int customerId, int accountId, bool trackChanges)
        {
            var account = await _repository.Account.GetAccountByCustomerIdAsync(customerId, accountId, trackChanges);
            if (account is null)
            {
                Log.Error($"Account with id {accountId} does not exist");
                throw new AccountNotFoundException(customerId, accountId);
            }
            var result = _mapper.Map<UserInfoDto>(account);
            return result;
        }
    }
}
