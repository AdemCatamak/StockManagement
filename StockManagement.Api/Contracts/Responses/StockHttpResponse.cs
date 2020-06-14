namespace StockManagement.Api.Contracts.Responses
{
    public class StockHttpResponse
    {
        public long StockId { get; set; }
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public int AvailableStock { get; set; }
    }
}