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
        Task<Transaction> GetTransactionAsync(int id, bool trackChanges);
    }
}
