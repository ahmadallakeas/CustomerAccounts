using Application.Configurations;
using Application.Interfaces.IServices;
using Domain.Entities;
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
    public class AuthService : IAuthService
    {

        private readonly JwtConfiguration _jwtConfiguration;

        public AuthService(IOptions<JwtConfiguration> configuration)
        {
            _jwtConfiguration = configuration.Value;
        }

        public async Task<string> CreateTokenAsync(Customer c)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(c);
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
        private async Task<List<Claim>> GetClaims(Customer customer)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,customer.FirstName+" "+customer.Surname),
                new Claim(ClaimTypes.Email,customer.Email)

            };
            return claims;
        }
    }
}
