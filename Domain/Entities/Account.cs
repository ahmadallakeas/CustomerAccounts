using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account
    {
        public string AccountId { get; set; }
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(Customer))]
        [Required(ErrorMessage = "This field CustomerId is required")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "This field Balance is required")]
        public double Balance { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
