using System;
using Automatonymous;

namespace SagaStateMachineWorkerService.Model
{
    public class OrderStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
    }
}