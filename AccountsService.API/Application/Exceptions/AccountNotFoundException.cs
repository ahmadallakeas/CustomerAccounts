using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(string id) : base($"The account with id {id} doesn't exist")
        {
        }
        public AccountNotFoundException(string customerId, string accountId) : base($"AccountId or customerId is incorrect")
        {
        }
    }
}
