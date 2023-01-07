using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransfer
{
    public record UserInfoDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public double Balance { get; set; }
        public ICollection<TransactionDto>? Transactions { get; set; }
    }
}
