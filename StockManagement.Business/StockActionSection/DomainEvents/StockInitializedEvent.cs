using System;
using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockInitializedEvent : INotification
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public DateTime StockActionDate { get; }

        public StockInitializedEvent(long productId, long stockActionId, DateTime stockActionDate)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            StockActionDate = stockActionDate;
        }
    }
}