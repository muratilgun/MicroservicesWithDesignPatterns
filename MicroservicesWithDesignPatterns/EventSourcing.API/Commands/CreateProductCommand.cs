using EventSourcing.API.DTOs;
using MediatR;

namespace EventSourcing.API.Commands
{
    public class CreateProductCommand : IRequest
    {
        public CreatedProductDto CreatedProductDto { get; set; }
    }
}
