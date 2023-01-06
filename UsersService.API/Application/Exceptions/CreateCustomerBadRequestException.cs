using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class CreateCustomerBadRequestException : BadRequestException
    {
        public CreateCustomerBadRequestException(string message) : base(message)
        {
        }
    }
}
