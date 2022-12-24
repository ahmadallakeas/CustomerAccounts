using Application.DataTransfer;
using Application.DataTransfer.RequestParams;
using Application.Interfaces.IRepository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountAsync(string id, bool trackChanges);
        Task<AccountDto> GetAccountForCustomerAsync(string customerId, string accountId, bool trackChanges);
        Task<AccountDto> CreateAccountForCustomer(string customerId, double initialCredits, bool trackChanges);
        Task<UserInfoDto> GetUserInfoAsync(string customerId, string accountId, bool trackChanges);
        Task<IEnumerable<AccountDto>> GetAccountsAsync(string customerId, bool trackChanges);
    }
}
