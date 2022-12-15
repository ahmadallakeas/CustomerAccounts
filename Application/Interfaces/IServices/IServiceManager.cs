using Application.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IServiceManager
    {
        IAccountService AccountService { get; }
        ITransactionService TransactionService { get; }
        IAuthenticationService AuthenticationService { get; }
        ICustomerService CustomerService { get; }
    }
}
