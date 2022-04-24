using MassTransit;
using System;

namespace Shared.Interfaces
{
    public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
    {
        
    }
}