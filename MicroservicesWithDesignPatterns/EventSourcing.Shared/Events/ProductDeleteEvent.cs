using System;

namespace EventSourcing.Shared.Events
{
    public class ProductDeleteEvent : IEvent
    {
        public Guid Id { get; set; }   
    }
}