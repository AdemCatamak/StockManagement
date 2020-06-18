using System;

namespace StockManagement.Business.StockSnapshotSection.Responses
{
    public class StockSnapshotResponse
    {
        public long StockId { get; }
        public long ProductId { get; }
        public int AvailableStock { get; }
        public DateTime LastStockActionDate { get; }


        public StockSnapshotResponse(long stockId, long productId, int availableStock, DateTime lastStockActionDate)
        {
            StockId = stockId;
            ProductId = productId;
            AvailableStock = availableStock;
            LastStockActionDate = lastStockActionDate;
        }
    }
}