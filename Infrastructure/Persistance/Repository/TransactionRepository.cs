using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Persistance.Repository
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }
        public void MakeTransaction(int accountId,Transaction transaction)
        {
            transaction.AccountId = accountId;
        }
        public void CreateTransaction(Transaction transaction)
        {
            Create(transaction);
        }

        public async Task<Transaction> GetTransactionAsync(int id, bool trackChanges)
        {
            return await FindByCondition(t => t.TransactionId == id, trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsForCustomerAsync(int customerId, bool trackChanges)
        {
            return await FindByCondition(t => t.AccountId == customerId, trackChanges)
                .OrderBy(t => t.TransactionName)
                .ToListAsync();
        }
    }
}
