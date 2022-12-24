using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "This field FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "This field Email is required")]
        public AuthenticationUser? User { get; set; }
        [ForeignKey(nameof(AuthenticationUser))]
        [Required(ErrorMessage = "This field AuthenticationUserId is required")]
        public string AuthenticationUserId { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
