using StockManagement.Business.StockSnapShotSection.Responses;
using StockManagement.Data.Models;

namespace StockManagement.Business.StockSnapShotSection.Mappings
{
    public static class StockSnapShotMappingExtensions
    {
        public static StockSnapshotResponse ToStockServiceResponse(this StockSnapShotModel stockSnapShotModel)
        {
            StockSnapshotResponse stockSnapshotResponse = null;
            if (stockSnapShotModel != null)
            {
                stockSnapshotResponse = new StockSnapshotResponse(stockSnapShotModel.Id, stockSnapShotModel.ProductId, stockSnapShotModel.AvailableStock);
            }

            return stockSnapshotResponse;
        }
    }
}