using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer.Response
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public LoginResponse(string firstName, string lastName, string email, TokenDto tokenDto, int expiresIn)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Token = tokenDto;
            ExpiresIn = expiresIn;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public TokenDto Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
