using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SqlRepository.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }
        public void MakeTransaction(string accountId, Transaction transaction)
        {

            transaction.AccountId = accountId;
            transaction.TransactionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Create(transaction);

        }


        public async Task<Transaction> GetTransactionAsync(string id, bool trackChanges)
        {
            return await FindByCondition(t => t.TransactionId == id, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsForCustomerAsync(string accountId, bool trackChanges)
        {
            return await FindByCondition(t => t.AccountId == accountId, trackChanges)
                .OrderBy(t => t.TransactionName)
                .ToListAsync();
        }
    }
}
