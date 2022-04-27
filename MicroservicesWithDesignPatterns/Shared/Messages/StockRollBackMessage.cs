﻿using System.Collections.Generic;

namespace Shared.Messages
{
    public class StockRollBackMessage : IStockRollBackMessage
    {
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}