using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using Application.Queries.AccountQueries;
using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.AccountHandlers
{
    internal sealed class GetAccountHandler : IQueryHandler<GetAccountQuery, AccountDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetAccountHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {

            var account = await _repository.Account.GetAccountAsync(request.accountId, request.trackChanges);
            if (account is null)
            {
                Log.Error($"Account with id {request.accountId} does not exist");
                throw new AccountNotFoundException(request.accountId);
            }
            var accountToReturn = _mapper.Map<AccountDto>(account);
            return accountToReturn;
        }
    }
}
