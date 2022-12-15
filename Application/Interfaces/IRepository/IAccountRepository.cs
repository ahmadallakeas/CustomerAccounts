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
        Task<IEnumerable<Account>> GetAccountsAsync(int customerid, bool trackChanges);
        Task<Account> GetAccountAsync(int id, bool trackChanges);
        Task<Account> GetAccountByCustomerId(int customerId, bool trackChanges);
        void CreateAccount(Account account, int customerId);
    }
}
