using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IOrderCreatedEvent
    {
        List<OrderItemMessage> OrderItems { get; set; }
    }
}