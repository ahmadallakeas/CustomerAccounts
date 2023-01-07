using Application.Exceptions;
using Application.Interfaces.IHandler;
using Application.Interfaces.IRepository;
using MediatR;
using Serilog;
using UsersService.API.Application.Commands.CustomersCommands;

namespace UsersService.API.Application.Handlers.CustomerHandlers
{
    public class UpdateCustomerTotalCommandHandler : ICommandHandler<UpdateCustomerTotalCommand, Unit>
    {
        private readonly IRepositoryManager _repository;

        public UpdateCustomerTotalCommandHandler(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCustomerTotalCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.Customer.GetCustomerAsync(request.customerId, request.trackChanges);
            if (customer is null)
            {
                Log.Error($"Customer with id {request.customerId} does not exist");
                throw new CustomerNotFoundException("id", request.customerId);
            }
            customer.Total += request.total;
            _repository.Customer.UpdateCustomerTotal(customer);
            await _repository.SaveAsync();
            return Unit.Value;
        }
    }
}
