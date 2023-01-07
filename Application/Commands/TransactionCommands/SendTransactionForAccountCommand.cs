using Application.Interfaces.IRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TransactionCommands
{
    public sealed record SendTransactionForAccountCommand(string accountId, bool trackChanges) : ICommand<Unit>;

}
