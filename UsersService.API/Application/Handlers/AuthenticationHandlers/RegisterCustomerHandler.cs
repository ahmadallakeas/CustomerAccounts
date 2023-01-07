using Application.Commands.AuthenticationCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using EntityFramework.Exceptions.Common;
using MassTransit;
using MediatR;
using Messages;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AuthenticationHandlers
{
    internal sealed class RegisterCustomerHandler : ICommandHandler<RegisterCustomerCommand, CustomerDto>
    {
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;
        private IPublishEndpoint _publishEndpoint;
        public RegisterCustomerHandler(IRepositoryManager repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<CustomerDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.customerForRegistration);
            try
            {
                _repository.Customer.CreateCustomer(customer);
                var result = await _repository.SaveAsync();
                var c = await _repository.Customer.GetCustomerByLoginAsync(request.customerForRegistration.Email, false);
                var customerToReturn = _mapper.Map<CustomerDto>(c);
                await _publishEndpoint.Publish<CustomerCreated>(new
                {
                    CustomerId = customerToReturn.CustomerId
                });
                return customerToReturn;
            }
            catch (UniqueConstraintException e)
            {
                Log.Error($"Customer with email{request.customerForRegistration.Email} already exists");
                throw new CreateCustomerBadRequestException($"Email {request.customerForRegistration.Email} already exists");
            }
            catch (MongoException e)
            {
                Log.Error($"Customer with email{request.customerForRegistration.Email} already exists");
                throw new CreateCustomerBadRequestException($"Email {request.customerForRegistration.Email} already exists");
            }
            catch (Exception e)
            {
                Log.Error($"An error occured {e.Message}");
                throw new CreateCustomerBadRequestException(e.Message);
            }
        }
    }
}
