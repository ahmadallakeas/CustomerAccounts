using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetTransactionAsync(string id, bool trackChanges);
        Task<IEnumerable<Transaction>> GetTransactionsForCustomerAsync(string customerId, bool trackChanges);
        void CreateTransaction(Transaction transaction);
        void MakeTransaction(string accountId, Transaction transaction);
    }
}
