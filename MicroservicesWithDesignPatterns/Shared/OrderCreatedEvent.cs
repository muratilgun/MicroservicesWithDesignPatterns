using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderCreatedEvent 
    {
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
