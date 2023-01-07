using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using Application.Queries.AccountQueries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Identity.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AccountHandlers
{
    internal class GetAccountForCustomerHandler : IQueryHandler<GetAccountForCustomerQuery, AccountDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetAccountForCustomerHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountForCustomerQuery request, CancellationToken cancellationToken)
        {
            var account = await _repository.Account.GetAccountByCustomerIdAsync(request.customerId, request.accountId, request.trackChanges);
            if (account is null)
            {
                Log.Error($"Account with id {request.accountId} does not exist");
                throw new AccountNotFoundException(request.customerId, request.accountId);
            }
            var accountToReturn = _mapper.Map<AccountDto>(account);
            return accountToReturn;
        }
    }
}
