namespace StockManagement.Api.Contracts.StockRequests
{
    public class AddToStockHttpRequest
    {
        public int Count { get; set; }
        public string CorrelationId { get; set; }
    }
}