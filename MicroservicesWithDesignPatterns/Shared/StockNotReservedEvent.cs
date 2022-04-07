namespace Shared
{
    public class StockNotReservedEvent
    {
        public int OrderId { get; set; }
        public string BuyerId { get; set; }
        public string Message { get; set; }
    }
}