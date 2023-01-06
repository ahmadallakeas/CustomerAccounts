using Application.Exceptions;
using Application.Interfaces.IRepository;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepository.Repository
{
    public class CustomersMongoRepository : ICustomerRepository
    {
        private IMongoCollection<Customer> _customers;
        //  private IMongoCollection<Account> _accounts;
        public CustomersMongoRepository(IMongoContext context)
        {
            _customers = context.Database.GetCollection<Customer>("Customers");
            //     _accounts = context.Database.GetCollection<Account>("Accounts");
        }

        public async Task<Customer> CheckPasswordAsync(string email, string password, bool trackChanges)
        {
            var builder = Builders<Customer>.Filter;
            var filter = builder.Eq(c => c.Email, email) & builder.Eq(c => c.Password, password);
            return await _customers.Find(filter).FirstOrDefaultAsync();
        }

        public void CreateCustomer(Customer customer)
        {
            _customers.InsertOne(customer);
        }

        public async Task<Customer> GetCustomerAsync(string customerId, bool trackChanges)
        {

            var customer = await _customers.Find(c => c.CustomerId == customerId).FirstOrDefaultAsync();
            if (customer is null)
            {
                Log.Error($"Customer with id {customerId} does not exist");
                throw new CustomerNotFoundException("id", customerId);
            }
            //  var accounts = await _accounts.Find(a => a.CustomerId == customerId).ToListAsync();
            // customer.Accounts = accounts;
            return customer;
        }

        public async Task<Customer> GetCustomerByLoginAsync(string email, bool trackChanges)
        {
            var builder = Builders<Customer>.Filter;
            var filter = builder.Eq(c => c.Email, email);
            var customer = await _customers.Find(filter).FirstOrDefaultAsync();
            if (customer is null)
            {
                Log.Error($"Customer with id {email} does not exist");
                throw new CustomerNotFoundException("email", email);
            }
            //   var accounts = await _accounts.Find(a => a.CustomerId == customer.CustomerId).ToListAsync();
            //   customer.Accounts = accounts;
            return customer;
        }
    }

}
