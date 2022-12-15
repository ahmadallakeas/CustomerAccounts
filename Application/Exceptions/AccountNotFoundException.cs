using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException(int id) : base($"The account with id {id} doesn't exist")
        {
        }
    }
}
