using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockCountDecreasedEvent : INotification
    {
        public long ProductId { get; private set; }
        public long StockActionId { get; private set; }
        public int Count { get; private set; }

        public StockCountDecreasedEvent(long productId, long stockActionId, int count)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
        }
    }
}