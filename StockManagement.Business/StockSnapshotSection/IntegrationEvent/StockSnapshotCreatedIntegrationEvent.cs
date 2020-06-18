using System;
using StockManagement.Utility.IntegrationEventPublisherSection;

namespace StockManagement.Business.StockSnapshotSection.IntegrationEvent
{
    public class StockSnapshotCreatedIntegrationEvent : IIntegrationEvent
    {
        public long ProductId { get; }
        public long StockSnapshotId { get; }
        public long StockActionId { get; }
        public DateTime StockCreatedOn { get; }

        public StockSnapshotCreatedIntegrationEvent(long productId, long stockSnapshotId, long stockActionId, DateTime stockCreatedOn)
        {
            ProductId = productId;
            StockSnapshotId = stockSnapshotId;
            StockActionId = stockActionId;
            StockCreatedOn = stockCreatedOn;
        }
    }
}