using Application.Interfaces.IRequest;
using Domain.Entities;
using MediatR;

namespace UsersService.API.Application.Commands.CustomersCommands
{
    public sealed record UpdateCustomerTotalCommand(string customerId, double total, bool trackChanges) : ICommand<Unit>
    {
    }
}
