using System;
using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockCountDecreasedEvent : INotification
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public int Count { get; }
        public DateTime StockActionDate { get; }

        public StockCountDecreasedEvent(long productId, long stockActionId, int count, DateTime stockActionDate)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
            StockActionDate = stockActionDate;
        }
    }
}