using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shared.Interfaces;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
