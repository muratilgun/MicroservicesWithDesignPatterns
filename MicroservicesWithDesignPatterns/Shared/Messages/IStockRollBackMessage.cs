using System.Collections.Generic;

namespace Shared.Messages
{
    public interface IStockRollBackMessage
    {
        public List<OrderItemMessage> OrderItems { get; set; }

    }
}