﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        [Required(ErrorMessage = "This field TransactionName is required")]
        public string TransactionName { get; set; }
        [Required(ErrorMessage = "This field Date is required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "This field Message is required")]
        public string Message { get; set; }
        public Account Account { get; set; }
        [ForeignKey(nameof(Account))]
        [Required(ErrorMessage = "This field AccountId is required")]
        public int AccountId { get; set; }
    }
}