using System;

namespace StockManagement.Business.StockSection.Responses
{
    public class StockResponse
    {
        public long StockId { get; }
        public long ProductId { get; }
        public string ProductCode { get; }
        public int AvailableStock { get; }
        public long StockActionId { get; }
        public DateTime LastStockOperationDate { get; }


        public StockResponse(long stockId, long productId, string productCode, int availableStock, long stockActionId, DateTime lastStockOperationDate)
        {
            StockId = stockId;
            ProductId = productId;
            ProductCode = productCode;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            LastStockOperationDate = lastStockOperationDate;
        }
    }
}