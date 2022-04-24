using System;
using System.Collections.Generic;
using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockReservedRequestPayment :CorrelatedBy<Guid>
    {
        public PaymentMessage Payment { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }

        public string BuyerId { get; set; }
    }
}