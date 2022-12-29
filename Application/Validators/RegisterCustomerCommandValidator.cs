using Application.Commands.AuthenticationCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public sealed class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(c => c.customerForRegistration).NotEmpty();
            RuleFor(c => c.customerForRegistration.FirstName).NotEmpty().WithMessage("This field FirstName is required").OverridePropertyName("FirstName");
            RuleFor(c => c.customerForRegistration.LastName).NotEmpty().WithMessage("This field LastName is required").OverridePropertyName("LastName");
            RuleFor(c => c.customerForRegistration.Email).NotEmpty().WithMessage("This field Email is required").OverridePropertyName("Email");
            RuleFor(c => c.customerForRegistration.Password).NotEmpty().WithMessage("This field Password is required").OverridePropertyName("Password");

            RuleFor(c => c.customerForRegistration.Email).EmailAddress().WithMessage("Invalid email").OverridePropertyName("Email");
            RuleFor(c => c.customerForRegistration.Password).MinimumLength(6).WithMessage("Password length should be greater than 6").OverridePropertyName("Password");





        }
    }
}
