using Application.Commands.AuthenticationCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public sealed class ValidateCustomerCommandValidator : AbstractValidator<ValidateCustomerCommand>
    {
        public ValidateCustomerCommandValidator()
        {
            RuleFor(c => c.customerForLogin.Email).NotEmpty().WithMessage("This field Email is required").OverridePropertyName("Email");
            RuleFor(c => c.customerForLogin.Password).NotEmpty().WithMessage("This field Password is required").OverridePropertyName("Password");

            RuleFor(c => c.customerForLogin.Email).EmailAddress().WithMessage("Invalid email").OverridePropertyName("Email");
            RuleFor(c => c.customerForLogin.Password).MinimumLength(6).WithMessage("Password length should be greater than 6").OverridePropertyName("Password");
        }
    }
}
