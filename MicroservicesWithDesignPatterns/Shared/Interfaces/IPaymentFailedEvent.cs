using System.Collections.Generic;

namespace Shared.Interfaces
{
    public interface IPaymentFailedEvent
    {
        public string Reason { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}