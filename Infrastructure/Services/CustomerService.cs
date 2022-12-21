using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CustomerService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerForRegistrationDto customerForRegistration, int userId, bool trackChanges)
        {
            var customer = _mapper.Map<Customer>(customerForRegistration);
            _repository.Customer.CreateCustomer(customer, userId);
            await _repository.SaveAsync();
            customer = await _repository.Customer.GetCustomerAsync(customer.CustomerId, trackChanges);

            var customerToReturn = _mapper.Map<CustomerDto>(customer);
            return customerToReturn;
        }

        public async Task<CustomerDto> GetCustomerAsync(int id, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerAsync(id, trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {id} does not exist");
                throw new CustomerNotFoundException(id);
            }
            var customerToReturn = _mapper.Map<CustomerDto>(customer);
            return customerToReturn;
        }

        public async Task<CustomerDto> GetCustomerByLoginAsync(int id, bool trackChanges)
        {
            var customer = await _repository.Customer.GetCustomerByLoginAsync(id, trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {id} does not exist");
                throw new CustomerNotFoundException(id);
            }
            var customerToReturn = _mapper.Map<CustomerDto>(customer);
            return customerToReturn;
        }
    }
}
