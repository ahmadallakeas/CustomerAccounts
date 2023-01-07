using AccountsService.API.Messages;
using Application.Commands.TransactionCommands;
using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MassTransit;
using MediatR;


namespace Application.Handlers.TransactionHandler
{
    internal class SendTransactionForAccountHandler : ICommandHandler<SendTransactionForAccountCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public SendTransactionForAccountHandler(IRepositoryManager repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Unit> Handle(SendTransactionForAccountCommand request, CancellationToken cancellationToken)
        {
            var account = await _repository.Account.GetAccountAsync(request.accountId, true);
            if (account == null)
            {
                throw new AccountNotFoundException(request.accountId);
            }
            Transaction transaction = new Transaction
            {
                TransactionName = "New Account Transaction",
                Message = $"New Transaction for account with id {request.accountId} with new value {request.balance}",
                Date = DateTime.Now.ToString("M/d/yyyy"),
            };
            _repository.Transaction.MakeTransaction(request.accountId, transaction);
            account.Balance += request.balance;
            _repository.Account.UpdateAccount(account);
            await _repository.SaveAsync();
            await _publishEndpoint.Publish<TotalChanged>(new {
                CustomerId=account.CustomerId,
                NewTotal=account.Balance
            });
            return await Task.FromResult(Unit.Value);
        }
    }
}
