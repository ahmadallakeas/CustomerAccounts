using Application.Interfaces.IRepository;
using Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
    public class TransactionsMongoRepository : ITransactionRepository
    {
        private readonly IMongoCollection<Account> _accounts;
        public TransactionsMongoRepository(IMongoContext context)
        {
            _accounts = context.Database.GetCollection<Account>("Accounts");

        }
        public Task<Transaction> GetTransactionAsync(string id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetTransactionsForCustomerAsync(string customerId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void MakeTransaction(string accountId, Transaction transaction)
        {
            transaction.AccountId = accountId;
            transaction.TransactionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            _accounts.UpdateOne(Builders<Account>.Filter.Eq(a => a.AccountId, accountId), Builders<Account>.Update.Push(a => a.Transactions, transaction));
        }
    }
}
