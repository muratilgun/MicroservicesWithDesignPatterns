using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;

namespace Payment.API.Consumer
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEvent> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(ILogger<StockReservedEvent> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            decimal balance = 3000m;
            if (balance> context.Message.Payment.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn from credit card for user id = {context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentSuccessedEvent {BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId});
            }
            else
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was not withdraw from credit card for user id = {context.Message.BuyerId}");
                await _publishEndpoint.Publish(new PaymentFailedEvent
                    { BuyerId = context.Message.BuyerId, OrderId = context.Message.OrderId, Message = "not enough balance"});
            }

        }

    }
}