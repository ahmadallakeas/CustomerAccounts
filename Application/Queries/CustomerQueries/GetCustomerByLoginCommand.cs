using Application.DataTransfer;
using Application.Interfaces.IRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.CustomerQueries
{
    public sealed record GetCustomerByLoginCommand(string email, bool trackChanges) : IQuery<CustomerDto>;

}
