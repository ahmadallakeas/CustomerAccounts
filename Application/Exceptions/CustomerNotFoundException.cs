using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(int id) : base($"The customer with id {id} doesn't exist")
        {
        }
    }
}
