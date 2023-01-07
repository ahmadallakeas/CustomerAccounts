using Application.Commands.TransactionCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public sealed class SendTransactionForAccountCommandValidator : AbstractValidator<SendTransactionForAccountCommand>
    {
        public SendTransactionForAccountCommandValidator()
        {
            RuleFor(c => c.accountId).NotEmpty();
        }
    }
}
