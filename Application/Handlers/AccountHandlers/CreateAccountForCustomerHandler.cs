using Application.Commands.AccountCommands;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
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
    internal sealed class CreateAccountForCustomerHandler : ICommandHandler<CreateAccountForCustomerCommand, AccountDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CreateAccountForCustomerHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> Handle(CreateAccountForCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Customer.GetCustomerAsync(request.customerId, request.trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {request.customerId} does not exist");
                throw new CustomerNotFoundException("id", request.customerId);

            }
            if (request.initialCredits < 0.0)
            {
                Log.Error($"Error in input data, initial credit cant be less than 0");
                throw new CreateAccountBadRequestException(request.initialCredits);
            }
            Account account = new Account
            {
                Balance = request.initialCredits,

            };
            _repository.Account.CreateAccount(account, request.customerId);
            await _repository.SaveAsync();
            var account1 = await _repository.Account.GetAccountAsync(account.AccountId, request.trackChanges);
            var accountToReturn = _mapper.Map<AccountDto>(account1);
            return accountToReturn;
        }
    }
}
