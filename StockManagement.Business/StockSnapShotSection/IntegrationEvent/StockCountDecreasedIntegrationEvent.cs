using StockManagement.Utility.IntegrationEventHandlerSection;

namespace StockManagement.Business.StockSnapShotSection.IntegrationEvent
{
    public class StockCountDecreasedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public int Count { get; }
        public int AvailableStock { get;  }

        public StockCountDecreasedIntegrationEvent(long productId, long stockActionId, int count, int availableStock)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
            AvailableStock = availableStock;
        }
    }
}