using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IRepositoryManager
    {
        IAccountRepository Account { get; }
        ICustomerRepository Customer { get; }
        ITransactionRepository Transaction { get; }
        Task SaveAsync();
    }
}
