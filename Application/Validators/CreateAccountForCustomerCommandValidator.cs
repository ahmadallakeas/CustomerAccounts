using Application.Commands.AccountCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public sealed class CreateAccountForCustomerCommandValidator : AbstractValidator<CreateAccountForCustomerCommand>
    {
        public CreateAccountForCustomerCommandValidator()
        {
            RuleFor(a => a.customerId).NotEmpty();
            RuleFor(a => a.initialCredits).NotEmpty();
            RuleFor(a => a.initialCredits).GreaterThanOrEqualTo(0);
        }
    }
}
