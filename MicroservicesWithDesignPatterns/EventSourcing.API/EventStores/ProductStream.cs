using System;
using EventSourcing.API.DTOs;
using EventSourcing.Shared.Events;
using EventStore.ClientAPI;

namespace EventSourcing.API.EventStores
{
    public class ProductStream  : AbstractStream
    {
        public static string StreamName => "ProductStream";
        public static string GroupName => "agroup";
        //public static string GroupName => "replay";
        public ProductStream(IEventStoreConnection eventStoreConnection) : base(eventStoreConnection, StreamName)
        {
        }

        public void Created(CreatedProductDto createdProductDto)
        {
            Events.AddLast(new ProductCreatedEvent
            {
                Id = Guid.NewGuid(),
                Name = createdProductDto.Name,
                Price = createdProductDto.Price,
                Stock = createdProductDto.Stock,
                UserId = createdProductDto.UserId
            });
        }

        public void NameChanged(ChangedProductNameDto changedProductNameDto)
        {
            Events.AddLast(new ProductNameChangedEvent
            {
                ChangedName = changedProductNameDto.Name,
                Id = changedProductNameDto.Id
            });
        }

        public void PriceChanged(ChangeProductPriceDto changeProductPriceDto)
        {
            Events.AddLast(new ProductPriceChangedEvent()
            {
                ChangedPrice = changeProductPriceDto.Price,
                Id = changeProductPriceDto.Id
            });
        }

        public void Deleted(Guid id)
        {
            Events.AddLast(new ProductDeleteEvent()
            {
                Id = id
            });
        }
    }
}