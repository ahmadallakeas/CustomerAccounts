using Application.Configurations;
using Application.DataTransfer;
using Application.Exceptions;
using Application.Interfaces.IRepository;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using EntityFramework.Exceptions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repository;
        private IMapper _mapper;
        private Customer? customer;
        private readonly JwtConfiguration _jwtConfiguration;

        public AuthenticationService(IMapper mapper, IOptions<JwtConfiguration> configuration, IRepositoryManager repository)
        {

            _mapper = mapper;
            _jwtConfiguration = configuration.Value;
            _repository = repository;
        }


        public async Task<CustomerDto> RegisterCustomerAsync(CustomerForRegistrationDto customerForRegistration)
        {
            customer = _mapper.Map<Customer>(customerForRegistration);
            _repository.Customer.CreateCustomer(customer);
            try
            {
                var result = await _repository.SaveAsync();
                var c = await _repository.Customer.GetCustomerByLoginAsync(customerForRegistration.Email, false);
                var customerToReturn = _mapper.Map<CustomerDto>(c);
                return customerToReturn;
            }
            catch (UniqueConstraintException e)
            {
                throw new CreateCustomerBadRequestException($"Email {customerForRegistration.Email} already exists");
            }
            catch (Exception e)
            {
                throw new CreateCustomerBadRequestException(e.Message);
            }
        }

        public async Task<bool> ValidateCustomerAsync(CustomerForLoginDto customerForLogin)
        {
            var customer1 = await _repository.Customer.GetCustomerByLoginAsync(customerForLogin.Email, false);
            if (customer1 is null)
            {
                Log.Error($"Customer with email {customerForLogin.Email} does not exist");
                throw new CustomerNotFoundException("email", customerForLogin.Email);
            }
            customer = customer1;
            return (customer != null && await _repository.Customer.CheckPasswordAsync(customerForLogin.Email, customerForLogin.Password, false) != null);
        }

        public async Task<CustomerDto> GetCustomerByEmail(string email)
        {
            var customer1 = await _repository.Customer.GetCustomerByLoginAsync(email, false);
            if (customer1 is null)
            {
                Log.Error($"Customer with email {email} does not exist");
                throw new CustomerNotFoundException("email", email);
            }
            return _mapper.Map<CustomerDto>(customer1);
        }

        public async Task<string> CreateTokenAsync(string customerId)
        {
            var customer1 = await _repository.Customer.GetCustomerAsync(customerId, false);
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(customer1);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return accessToken;

        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
                    issuer: _jwtConfiguration.ValidIssuer,
                     claims: claims,
                    audience: _jwtConfiguration.ValidAudience,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                    signingCredentials: signingCredentials
                );
            return tokenOptions;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CAKEY"));
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaims(Customer c)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,c.FirstName+" "+c.Surname),
                new Claim(ClaimTypes.Email,c.Email)

            };
            return claims;
        }

    }
}
