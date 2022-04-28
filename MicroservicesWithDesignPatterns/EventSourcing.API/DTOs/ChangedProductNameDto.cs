using System;

namespace EventSourcing.API.DTOs
{
    public class ChangedProductNameDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}