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
        Task<CustomerDto> GetCustomerByLoginAsync(int accountId, bool trackChanges);
        Task<CustomerDto> GetCustomerAsync(int id, bool trackChanges);
        Task<CustomerDto> CreateCustomerAsync(CustomerForRegistrationDto customerForRegistration, int userId, bool trackChanges);
    }
}
