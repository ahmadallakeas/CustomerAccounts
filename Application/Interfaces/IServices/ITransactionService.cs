using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionAsync(string id, bool trackChanges);
        Task SendTransactionForAccount(string accountId, bool trackChanges);
    }
}
