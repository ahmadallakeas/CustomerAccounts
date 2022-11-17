﻿using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAccountService> _accountService;
        private readonly Lazy<TransactionService> _transactionService;
        public ServiceManager(IRepositoryManager repositoryManager,IMapper mapper)
        {
            _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager,mapper));
            _transactionService = new Lazy<TransactionService>(() => new TransactionService(repositoryManager,mapper));  
        }
        public IAccountService AccountService => _accountService.Value;
        public ITransactionService TransactionService => _transactionService.Value;
    }
}