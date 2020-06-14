namespace StockManagement.Api.Contracts.StockActionRequests
{
    public class AddToStockHttpRequest
    {
        public int Count { get; set; }
        public string CorrelationId { get; set; }
    }
}