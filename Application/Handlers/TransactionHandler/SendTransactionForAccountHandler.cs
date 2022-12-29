﻿using Application.Commands.TransactionCommands;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Handlers.TransactionHandler
{
    internal class SendTransactionForAccountHandler : ICommandHandler<SendTransactionForAccountCommand, Unit>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public SendTransactionForAccountHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SendTransactionForAccountCommand request, CancellationToken cancellationToken)
        {
            Transaction transaction = new Transaction
            {
                TransactionName = "New Account Transaction",
                Message = $"New Transaction for account with id {request.accountId}",
                Date = DateTime.Now.ToString("M/d/yyyy"),
            };
            _repository.Transaction.MakeTransaction(request.accountId, transaction);
            await _repository.SaveAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}