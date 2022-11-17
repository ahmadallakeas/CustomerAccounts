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
        Task<AccountDto> GetAccountAsync(int id, bool trackChanges);
        Task<AccountDto> CreateAccountForCustomer(RequestBody requestBody, bool trackChanges);
        Task<UserInfoDto> GetUserInfoAsync(int id, bool trackChanges);
    }
}
