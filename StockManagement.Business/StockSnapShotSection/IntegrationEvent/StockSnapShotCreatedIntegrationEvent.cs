using System;
using StockManagement.Utility.IntegrationEventPublisherSection;

namespace StockManagement.Business.StockSnapShotSection.IntegrationEvent
{
    public class StockSnapShotCreatedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; }
        public long StockSnapShotId { get; }
        public long StockActionId { get; }
        public DateTime StockCreatedOn { get; }

        public StockSnapShotCreatedIntegrationEvent(long productId, long stockSnapShotId, long stockActionId, DateTime stockCreatedOn)
        {
            ProductId = productId;
            StockSnapShotId = stockSnapShotId;
            StockActionId = stockActionId;
            StockCreatedOn = stockCreatedOn;
        }
    }
}