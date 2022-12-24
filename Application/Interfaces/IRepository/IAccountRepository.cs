using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsAsync(string customerid, bool trackChanges);
        Task<Account> GetAccountAsync(string id, bool trackChanges);

        Task<Account> GetAccountByCustomerIdAsync(string customerId, string accountId, bool trackChanges);
        void CreateAccount(Account account, string customerId);
    }
}
