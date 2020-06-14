namespace StockManagement.Business.ProductSection.Responses
{
    public class ProductResponse
    {
        public long ProductId { get; }
        public string ProductCode { get; }

        public ProductResponse(long productId, string productCode)
        {
            ProductId = productId;
            ProductCode = productCode;
        }
    }
}