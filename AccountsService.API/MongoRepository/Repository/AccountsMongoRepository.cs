using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.Identity.Client;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
    public class AccountsMongoRepository : IAccountRepository
    {
        private IMongoCollection<Account> _accounts;

        public AccountsMongoRepository(IMongoContext context)
        {
            _accounts = context.Database.GetCollection<Account>("Accounts");
        }

        public void CreateAccount(Account account, string customerId)
        {
            account.CustomerId = customerId;
            _accounts.InsertOne(account);
        }

        public async Task<Account> GetAccountAsync(string id, bool trackChanges)
        {

            var builder = Builders<Account>.Filter;
            var filter = builder.Eq(a => a.AccountId, id);

            return await _accounts.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Account> GetAccountByCustomerIdAsync(string customerId, string accountId, bool trackChanges)
        {
            var builder = Builders<Account>.Filter;
            var filter = builder.Eq(a => a.AccountId, accountId) & builder.Eq(a => a.CustomerId, customerId);
            //var account = _accounts.Aggregate()
            //    .Match(filter)
            //    .Lookup("Customers", "CustomerId", "_id", "Customer").
            //    Unwind("Customer").As<Account>().FirstOrDefaultAsync();

            //return await account;
            var account = await _accounts.Find(filter).FirstOrDefaultAsync();
            return account;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(string customerid, bool trackChanges)
        {
            return await _accounts.Find(a => a.CustomerId == customerid).ToListAsync();
        }

        public void UpdateAccount(Account account)
        {
            _accounts.UpdateOne(Builders<Account>.Filter.Eq(a => a.AccountId, account.AccountId), Builders<Account>.Update.Set(a => a.Balance, account.Balance));


        }
    }
}
