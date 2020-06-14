using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockCountIncreasedEvent : INotification
    {
        public long ProductId { get; private set; }
        public long StockActionId { get; private set; }
        public int Count { get; private set; }

        public StockCountIncreasedEvent(long productId, long stockActionId, int count)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
        }
    }
}