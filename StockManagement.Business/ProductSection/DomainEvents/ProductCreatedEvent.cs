using MediatR;

namespace StockManagement.Business.ProductSection.DomainEvents
{
    public class ProductCreatedEvent : INotification
    {
        public long ProductId { get; private set; }
        public string ProductCode { get; private set; }

        public ProductCreatedEvent(long productId, string productCode)
        {
            ProductId = productId;
            ProductCode = productCode;
        }
    }
}