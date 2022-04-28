using System;

namespace EventSourcing.Shared.Events
{
    public class ProductPriceChangedEvent
    {
        public Guid Id { get; set; }
        public decimal ChangedPrice { get; set; }
    }
}