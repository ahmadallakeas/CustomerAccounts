using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class TransactionService : ITransactionService
    {
        private readonly IRepositoryManager _repository;

        public TransactionService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Transaction> GetTransactionAsync(int id, bool trackChanges)
        {
            var transaction = await _repository.Transaction.GetTransactionAsync(id, trackChanges);
            return transaction;
        }
    }
}
