using Application.DataTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.AccountQueries
{
    public sealed record GetAccountForCustomerQuery(string customerId, string accountId, bool trackChanges) : IRequest<AccountDto>;
}
