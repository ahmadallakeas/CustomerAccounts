using AccountsService.API.Messages;
using MassTransit;
using MediatR;
using UsersService.API.Application.Commands.CustomersCommands;

namespace UsersService.API.Consumers
{
    public class TotalChangedConsumer : IConsumer<TotalChanged>
    {
        private readonly ISender _sender;

        public TotalChangedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<TotalChanged> context)
        {
            await _sender.Send(new UpdateCustomerTotalCommand(context.Message.CustomerId, context.Message.NewBalance, true));
        }
    }
}
