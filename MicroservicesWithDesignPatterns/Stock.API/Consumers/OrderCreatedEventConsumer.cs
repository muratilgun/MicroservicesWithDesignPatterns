using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Shared;

namespace Stock.API.Consumers
{
    public class OrderCreatedEventConsumer :  IConsumer<OrderCreatedEvent>
    {

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
