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

        public void CreateCustomer(Customer customer, string userId)
        {
            customer.AuthenticationUserId = userId;
            customer.CustomerId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            Create(customer);
        }

        public async Task<Customer> GetCustomerAsync(string customerId, bool trackChanges)
        {
            return await FindByCondition(c => c.CustomerId == customerId, trackChanges)
                .Include(c => c.User)
                .Include(c => c.Accounts)
                .SingleOrDefaultAsync();
        }
        public async Task<Customer> GetCustomerByLoginAsync(string id, bool trackChanges)
        {
            return await FindByCondition(c => c.AuthenticationUserId == id, trackChanges)
              .Include(c => c.User)
              .Include(c => c.Accounts)
              .SingleOrDefaultAsync();
        }
    }
}
