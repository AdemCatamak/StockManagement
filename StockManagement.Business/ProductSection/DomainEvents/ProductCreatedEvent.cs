using MediatR;

namespace StockManagement.Business.ProductSection.DomainEvents
{
    public class ProductCreatedEvent : INotification
    {
        public long ProductId { get; }
        public string ProductCode { get; }

        public ProductCreatedEvent(long productId, string productCode)
        {
            ProductId = productId;
            ProductCode = productCode;
        }
    }
}