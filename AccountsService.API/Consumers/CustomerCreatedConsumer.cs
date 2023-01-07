using Application.Commands.AccountCommands;
using MassTransit;
using MediatR;
using Messages;

namespace AccountsService.API.Consumers
{
    public class CustomerCreatedConsumer : IConsumer<CustomerCreated>
    {
        private readonly ISender _sender;

        public CustomerCreatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<CustomerCreated> context)
        {
            await _sender.Send(new CreateAccountForCustomerCommand(context.Message.CustomerId, 0, false));

        }
    }
}
