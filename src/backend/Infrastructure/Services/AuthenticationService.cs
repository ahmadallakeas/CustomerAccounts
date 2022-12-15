﻿using Application.Configurations;
using Application.DataTransfer;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        private readonly UserManager<AuthenticationUser> _manager;
        private AuthenticationUser? _user;
        private IMapper _mapper;
        private readonly JwtConfiguration _jwtConfiguration;

        public AuthenticationService(UserManager<AuthenticationUser> manager, IMapper mapper, IOptions<JwtConfiguration> configuration)
        {
            _manager = manager;
            _mapper = mapper;
            _jwtConfiguration = configuration.Value;
        }


        public async Task<IdentityResult> RegisterCustomerAsync(CustomerForRegistrationDto customerForRegistration)
        {
            var userToCreate = new AuthenticationUser();
            userToCreate.Email = customerForRegistration.Email;
            userToCreate.UserName = customerForRegistration.Email;
            var result = await _manager.CreateAsync(userToCreate, customerForRegistration.Password);
            return result;
        }

        public async Task<bool> ValidateCustomerAsync(CustomerForLoginDto customerForLogin)
        {
            _user = await _manager.FindByEmailAsync(customerForLogin.Email);
            return (_user != null && await _manager.CheckPasswordAsync(_user, customerForLogin.Password));
        }

        public async Task<AuthenticationUser> GetAuthenticationUserAsync(string email)
        {
            return await _manager.FindByEmailAsync(email);
        }
        public async Task<TokenDto> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return new TokenDto(accessToken);

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
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            //var roles = await _manager.GetRolesAsync(_user);
            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}
            return claims;
        }

    }
}