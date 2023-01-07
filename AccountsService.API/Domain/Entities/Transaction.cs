using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities
{
    public class Transaction
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionId { get; set; }
        [Required(ErrorMessage = "This field TransactionName is required")]
        public string TransactionName { get; set; }
        [Required(ErrorMessage = "This field Date is required")]
        public string Date { get; set; }
        [Required(ErrorMessage = "This field Message is required")]
        public string Message { get; set; }
        [BsonIgnore]
        public Account Account { get; set; }
        [ForeignKey(nameof(Account))]
        [Required(ErrorMessage = "This field AccountId is required")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountId { get; set; }
    }
}
