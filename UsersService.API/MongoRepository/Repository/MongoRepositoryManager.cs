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
        private readonly Lazy<ICustomerRepository> _customerRepository;
        public MongoRepositoryManager(IMongoContext context)
        {
            _customerRepository = new Lazy<ICustomerRepository>(() => new CustomersMongoRepository(context));
        }

        public ICustomerRepository Customer => _customerRepository.Value;


        public async Task<int> SaveAsync()
        {
            return -1;
        }
    }
}
