using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(
              new Customer
              {
                  CustomerId = 1,
                  FirstName = "Ahmad",
                  Surname = "Al Lakeas"
              },
               new Customer
               {
                   CustomerId = 2,
                   FirstName = "John",
                   Surname = "Michael"
               },
                new Customer
                {
                    CustomerId =3,
                    FirstName = "Jalen",
                    Surname = "Green"
                },
                 new Customer
                 {
                     CustomerId = 4,
                     FirstName = "Adam",
                     Surname = "Watson"
                 },
                  new Customer
                  {
                      CustomerId = 5,
                      FirstName = "Steve",
                      Surname = "Darwin"
                  }
                );
        }
    }
}
