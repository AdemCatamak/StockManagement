using System;
using StockManagement.Utility.IntegrationEventPublisherSection;

namespace StockManagement.Business.StockSnapshotSection.IntegrationEvent
{
    public class StockInitializedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public DateTime StockCreatedOn { get; }

        public StockInitializedIntegrationEvent(long productId, long stockActionId, DateTime stockCreatedOn)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            StockCreatedOn = stockCreatedOn;
        }
    }
}