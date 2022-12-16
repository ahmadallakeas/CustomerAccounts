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

        public LoginResponse(string firstName, string lastName, string email, string token, int expiresIn, int customerId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Token = token;
            ExpiresIn = expiresIn;
            CustomerId = customerId;
        }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int ExpiresIn { get; set; }
    }
}
