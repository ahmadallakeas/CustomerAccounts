using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRepository.Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public void CreateAccount(Account account, string customerId)
        {
            account.CustomerId = customerId;
            account.AccountId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Create(account);
        }

        public async Task<Account> GetAccountAsync(string id, bool trackChanges)
        {
            return await FindByCondition(u => u.AccountId == id, trackChanges)
                .Include(t => t.Transactions)
                .FirstOrDefaultAsync();
        }
        public async Task<Account> GetAccountByCustomerIdAsync(string customerId, string accountId, bool trackChanges)
        {
            return await FindByCondition(u => u.CustomerId == customerId && u.AccountId == accountId, trackChanges)
             .Include(t => t.Transactions)
             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(string customerId, bool trackChanges)
        {
            return await FindByCondition(a => a.CustomerId == customerId, trackChanges)
            .Include(t => t.Transactions)
            .ToListAsync();
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
        }
    }
}
