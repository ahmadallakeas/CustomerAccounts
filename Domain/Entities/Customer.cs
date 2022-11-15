using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "This field FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field Surname is required")]
        public string Surname { get; set; }
    }
}
