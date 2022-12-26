using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByLoginAsync(string email, bool trackChanges);
        Task<CustomerDto> GetCustomerAsync(string id, bool trackChanges);
        Task<CustomerDto> CreateCustomerAsync(CustomerForRegistrationDto customerForRegistration, string userId, bool trackChanges);
    }
}
