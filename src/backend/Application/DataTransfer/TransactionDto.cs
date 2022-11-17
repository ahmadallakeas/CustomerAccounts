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
    public record TransactionDto
    {
        public int TransactionId { get; set; }
        public string TransactionName { get; set; }
        public string Date { get; set; }
        public string Message { get; set; }
        public int AccountId { get; set; }
    }
}
