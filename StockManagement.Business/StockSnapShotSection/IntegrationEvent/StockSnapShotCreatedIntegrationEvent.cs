using StockManagement.Utility.IntegrationEventHandlerSection;

namespace StockManagement.Business.StockSnapShotSection.IntegrationEvent
{
    public class StockSnapShotCreatedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; }
        public long StockSnapShotId { get; }
        public long StockActionId { get; }

        public StockSnapShotCreatedIntegrationEvent(long productId, long stockSnapShotId, long stockActionId)
        {
            ProductId = productId;
            StockSnapShotId = stockSnapShotId;
            StockActionId = stockActionId;
        }
    }
}