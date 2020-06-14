using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockInitializedEvent : INotification
    {
        public long ProductId { get; }
        public long StockActionId { get; }

        public StockInitializedEvent(long productId, long stockActionId)
        {
            ProductId = productId;
            StockActionId = stockActionId;
        }
    }
}