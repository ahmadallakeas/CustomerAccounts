using Application.Configurations;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
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
        private readonly Lazy<ITransactionService> _transactionService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICustomerService> _customerService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, UserManager<AuthenticationUser> userManager, IOptions<JwtConfiguration> configuration)
        {
            _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager, mapper));
            _transactionService = new Lazy<ITransactionService>(() => new TransactionService(repositoryManager, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, mapper, configuration));
            _customerService = new Lazy<ICustomerService>(() => new CustomerService(repositoryManager, mapper));
        }
        public IAccountService AccountService => _accountService.Value;
        public ITransactionService TransactionService => _transactionService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public ICustomerService CustomerService => _customerService.Value;
    }
}
