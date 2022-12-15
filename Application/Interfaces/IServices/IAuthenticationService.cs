using Application.DataTransfer;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterCustomerAsync(CustomerForRegistrationDto customerForRegistration);
        Task<bool> ValidateCustomerAsync(CustomerForLoginDto customerForLogin);
        Task<TokenDto> CreateTokenAsync();
        Task<AuthenticationUser> GetAuthenticationUserAsync(string email);
    }
}
