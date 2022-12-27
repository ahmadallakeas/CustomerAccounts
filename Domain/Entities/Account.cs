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
    public class Account
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string AccountId { get; set; }
        [BsonIgnoreIfNull]
        public Customer? Customer { get; set; }
        [ForeignKey(nameof(Customer))]
        [Required(ErrorMessage = "This field CustomerId is required")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "This field Balance is required")]
        public double Balance { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<Transaction>? Transactions { get; set; }
    }
}
