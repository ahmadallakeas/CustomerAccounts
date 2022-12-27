using Application.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(CustomerDto).IsAssignableFrom(type)
                || typeof(IEnumerable<CustomerDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<CustomerDto>)
            {
                foreach (var company in (IEnumerable<CustomerDto>)context.Object)
                {
                    FormatCustomerCsv(buffer, company);
                }
            }
            else
            {
                FormatCustomerCsv(buffer, (CustomerDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCustomerCsv(StringBuilder buffer, CustomerDto customer)
        {
            //Didn't format the accounts and transactions into csv because I dont know the proper structure of csv nested arrays.
            buffer.AppendLine($"{customer.CustomerId},\"{customer.FirstName} {customer.LastName},\"{customer.Email}\"");
        }

    }
}
