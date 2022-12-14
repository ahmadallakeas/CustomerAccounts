using Application.DataTransfer;
using Application.Interfaces.IRequest;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AccountCommands
{
    public sealed record CreateAccountForCustomerCommand(string customerId, double initialCredits, bool trackChanges) : ICommand<AccountDto>;


}
