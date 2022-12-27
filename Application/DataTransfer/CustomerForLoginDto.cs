using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public record CustomerForLoginDto
    {
        [Required(ErrorMessage = "The field Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field Password is required")]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage = "Minimum length is 6")]

        public string Password { get; set; }
    }
}
