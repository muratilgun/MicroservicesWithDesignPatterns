using System;
using Shared.Interfaces;

namespace Shared
{
    public class StockNotReservedEvent :IStockNotReservedEvent
    {
        public StockNotReservedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; }
        public string Reason { get; set; }
    }
}