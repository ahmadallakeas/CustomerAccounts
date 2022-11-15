using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
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

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _accountService = new Lazy<IAccountService>(() => new AccountService(repositoryManager));
        }
        public IAccountService AccountService => _accountService.Value;
    }
}
