using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using Application.Queries.AccountQueries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AccountHandlers
{
    internal sealed class GetAccountsHandler : IQueryHandler<GetAccountsQuery, IEnumerable<AccountDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetAccountsHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AccountDto>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            //var customer = await _repository.Customer.GetCustomerAsync(request.customerId, request.trackChanges);
            //if (customer is null)
            //{
            //    Log.Error($"Customer with id {request.customerId} does not exist");
            //    throw new CustomerNotFoundException("id", request.customerId);
            //}
            var accounts = await _repository.Account.GetAccountsAsync(request.customerId, request.trackChanges);
            var accountsDto = _mapper.Map<IEnumerable<AccountDto>>(accounts);
            return accountsDto;
        }
    }
}
