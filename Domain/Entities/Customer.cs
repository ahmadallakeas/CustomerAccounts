using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "This field FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field Surname is required")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "This field Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field Password is required")]
        public string Password { get; set; }

        //public AuthenticationUser? User { get; set; }
        //[ForeignKey(nameof(AuthenticationUser))]
        //[Required(ErrorMessage = "This field AuthenticationUserId is required")]
        //public string AuthenticationUserId { get; set; }
        [BsonIgnoreIfNullAttribute]
        public ICollection<Account>? Accounts { get; set; }
    }
}
