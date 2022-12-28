using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Queries.CustomerQueries;
using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CustomerHandlers
{
    internal sealed class GetCustomerByLoginHandler : IRequestHandler<GetCustomerByLoginCommand, CustomerDto>
    {
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;

        public GetCustomerByLoginHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerByLoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Customer.GetCustomerByLoginAsync(request.email, request.trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with email {request.email} does not exist");
                throw new CustomerNotFoundException("email", request.email);
            }
            var customerToReturn = _mapper.Map<CustomerDto>(customer);
            return customerToReturn;

        }
    }
}
