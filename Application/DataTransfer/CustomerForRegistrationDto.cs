using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public record CustomerForRegistrationDto
    {
        [Required(ErrorMessage = "This field FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field Surname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field Password is required")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password minimum length is 6")]
        public string Password { get; set; }
    }
}
