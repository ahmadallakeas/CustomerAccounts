using Application.DataTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AuthenticationCommands
{
    public sealed record RegisterCustomerCommand(CustomerForRegistrationDto customerForRegistration) : IRequest<CustomerDto>;

}
