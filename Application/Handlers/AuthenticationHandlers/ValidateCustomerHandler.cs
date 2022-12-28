
using Application.Commands.AuthenticationCommands;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticationHandlers
{
    internal sealed class ValidateCustomerHandler : IRequestHandler<ValidateCustomerCommand, bool>
    {
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;

        public ValidateCustomerHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ValidateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer1 = await _repository.Customer.GetCustomerByLoginAsync(request.customerForLogin.Email, false);
            if (customer1 is null)
            {
                Log.Error($"Customer with email {request.customerForLogin.Email} does not exist");
                throw new CustomerNotFoundException("email", request.customerForLogin.Email);
            }
            return (customer1 != null && await _repository.Customer.CheckPasswordAsync(request.customerForLogin.Email, request.customerForLogin.Password, false) != null);
        }
    }
}
