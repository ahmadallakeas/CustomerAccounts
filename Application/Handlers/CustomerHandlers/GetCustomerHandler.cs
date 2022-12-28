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
    internal sealed class GetCustomerHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
    {
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;

        public GetCustomerHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Customer.GetCustomerAsync(request.customerId, request.trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {request.customerId} does not exist");
                throw new CustomerNotFoundException("id", request.customerId);
            }

            var customerToReturn = _mapper.Map<CustomerDto>(customer);
            return customerToReturn;
        }
    }
}
