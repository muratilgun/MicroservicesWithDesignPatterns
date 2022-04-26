using System;
using Shared.Interfaces;

namespace Shared
{
    public class PaymentCompletedEvent :  IPaymentCompletedEvent
    {
        public PaymentCompletedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public Guid CorrelationId { get; }
    }
}