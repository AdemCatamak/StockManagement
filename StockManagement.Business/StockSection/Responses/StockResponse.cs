namespace StockManagement.Business.StockSection.Responses
{
    public class StockResponse
    {
        public long StockId { get; }
        public long ProductId { get;  }
        public string ProductCode { get; }
        public int AvailableStock { get; }
        public long StockActionId { get; }

        public StockResponse(long stockId, long productId, string productCode, int availableStock, long stockActionId)
        {
            StockId = stockId;
            ProductId = productId;
            ProductCode = productCode;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
        }
    }
}