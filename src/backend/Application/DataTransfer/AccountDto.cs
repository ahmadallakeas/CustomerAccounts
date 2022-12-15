using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public record AccountDto
    {
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public double Balance { get; set; }
        public ICollection<TransactionDto>? Transactions { get; set; }
    }
}
