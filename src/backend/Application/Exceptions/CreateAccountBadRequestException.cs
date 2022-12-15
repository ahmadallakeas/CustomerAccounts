using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public sealed class CreateAccountBadRequestException : BadRequestException
    {
        public CreateAccountBadRequestException(double initialCredit) : base($"The value {initialCredit} is not valid, Please enter a valid value")
        {
        }
    }
}
