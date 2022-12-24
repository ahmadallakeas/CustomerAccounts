using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(string customerId, bool trackChanges);
        Task<Customer> GetCustomerByLoginAsync(string id, bool trackChanges);
        void CreateCustomer(Customer customer, string userId);
    }
}
