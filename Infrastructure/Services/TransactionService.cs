using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public TransactionService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Transaction> GetTransactionAsync(int id, bool trackChanges)
        {
            var transaction = await _repository.Transaction.GetTransactionAsync(id, trackChanges);
            return transaction;
        }
        public async Task SendTransactionForAccount(int accountId, bool trackChanges)
        {
            Transaction transaction = new Transaction
            {
                TransactionName = "New Account Transaction",
                Message = $"New Transaction for account with id {accountId}",
                Date = DateTime.Now.ToString("M/d/yyyy"),
            };
            _repository.Transaction.MakeTransaction(accountId, transaction);
            await _repository.SaveAsync();
        }
    }
}
