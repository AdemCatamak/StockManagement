using StockManagement.Utility.IntegrationEventHandlerSection;

namespace StockManagement.Business.StockSnapShotSection.IntegrationEvent
{
    public class StockCountIncreasedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; private set; }
        public long StockActionId { get; private set; }
        public int Count { get; private set; }
        public int AvailableStock { get; }

        public StockCountIncreasedIntegrationEvent(long productId, long stockActionId, int count, int availableStock)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
            AvailableStock = availableStock;
        }
    }
}