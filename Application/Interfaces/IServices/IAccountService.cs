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
        Task<Account> GetAccountAsync(int id, bool trackChanges);
        Task<Account> CreateAccountForCustomer(RequestBody requestBody, bool trackChanges);
    }
}
