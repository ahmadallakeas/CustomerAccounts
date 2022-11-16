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
        Task<Transaction> GetTransactionAsync(int id, bool trackChanges);
        Task<IEnumerable<Transaction>> GetTransactionsForCustomerAsync(int customerId,bool trackChanges);
        void CreateTransaction(Transaction transaction);
    }
}
