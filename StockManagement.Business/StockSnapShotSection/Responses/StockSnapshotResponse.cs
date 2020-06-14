namespace StockManagement.Business.StockSnapShotSection.Responses
{
    public class StockSnapshotResponse
    {
        public long StockId { get; private set; }
        public long ProductId { get; private set; }
        public int AvailableStock { get; private set; }

        public StockSnapshotResponse(long stockId, long productId, int availableStock)
        {
            StockId = stockId;
            ProductId = productId;
            AvailableStock = availableStock;
        }
    }
}