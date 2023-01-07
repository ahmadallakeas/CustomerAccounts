using Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
    public class MongoRepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        public MongoRepositoryManager(IMongoContext context)
        {
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountsMongoRepository(context));
            _transactionRepository = new Lazy<ITransactionRepository>(() => new TransactionsMongoRepository(context));
        }
        public IAccountRepository Account => _accountRepository.Value;

        public ITransactionRepository Transaction => _transactionRepository.Value;

        public async Task<int> SaveAsync()
        {
            return -1;
        }
    }
}
