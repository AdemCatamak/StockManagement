namespace StockManagement.Api.Contracts.StockActionRequests
{
    public class RemoveFromStockHttpRequest
    {
        public int Count { get; set; }
        public string CorrelationId { get; set; }
    }
}