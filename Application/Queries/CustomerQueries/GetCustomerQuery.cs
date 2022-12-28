using Application.DataTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CustomerQueries
{
    public sealed record GetCustomerQuery(string customerId, bool trackChanges) : IRequest<CustomerDto>;

}
