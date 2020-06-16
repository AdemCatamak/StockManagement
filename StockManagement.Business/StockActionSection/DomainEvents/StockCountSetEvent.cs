using System;
using MediatR;

namespace StockManagement.Business.StockActionSection.DomainEvents
{
    public class StockCountSetEvent : INotification
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public int Count { get; }
        public DateTime StockActionDate { get; }

        public StockCountSetEvent(long productId, long stockActionId, int count, DateTime stockActionDate)
        {
            ProductId = productId;
            StockActionId = stockActionId;
            Count = count;
            StockActionDate = stockActionDate;
        }
    }
}