using Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IAccountRepository> _accountRepository;
        private readonly Lazy<ICustomerRepository> _customerRepository;
        private readonly Lazy<ITransactionRepository> _transactionRepository;
        public RepositoryManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _accountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(dbContext));
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomerRepository(dbContext));
            _transactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(dbContext));
        }

        public IAccountRepository Account => _accountRepository.Value;
        public ICustomerRepository Customer => _customerRepository.Value;
        public ITransactionRepository Transaction => _transactionRepository.Value;

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
