using StockManagement.Business.StockSnapshotSection.Responses;
using StockManagement.Data.Models;

namespace StockManagement.Business.StockSnapshotSection.Mappings
{
    public static class StockSnapshotMappingExtensions
    {
        public static StockSnapshotResponse ToStockServiceResponse(this StockSnapshotModel stockSnapshotModel)
        {
            StockSnapshotResponse stockSnapshotResponse = null;
            if (stockSnapshotModel != null)
            {
                stockSnapshotResponse = new StockSnapshotResponse(stockSnapshotModel.Id, stockSnapshotModel.ProductId, stockSnapshotModel.AvailableStock, stockSnapshotModel.LastStockActionDate);
            }

            return stockSnapshotResponse;
        }
    }
}