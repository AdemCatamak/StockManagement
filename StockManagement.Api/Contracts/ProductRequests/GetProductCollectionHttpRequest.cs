namespace StockManagement.Api.Contracts.ProductRequests
{
    public class GetProductCollectionHttpRequest : BaseCollectionHttpRequest
    {
        public string ProductCode { get; set; }
    }
}