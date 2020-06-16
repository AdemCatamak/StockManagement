namespace StockManagement.Api.Contracts.StockRequests
{
    public class RemoveFromStockHttpRequest
    {
        public int Count { get; set; }
        public string CorrelationId { get; set; }
    }
}