using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
        }

        public async Task<Customer> GetCustomerAsync(int customerId, bool trackChanges)
        {
            return await FindByCondition(c => c.CustomerId == customerId, trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
