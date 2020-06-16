using StockManagement.Business.StockSection.Responses;
using StockManagement.Data.Models;

namespace StockManagement.Business.StockSection.Mappings
{
    public static class StockMappingExtensions
    {
        public static StockResponse ToStockResponse(this StockModel stockModel)
        {
            if (stockModel == null)
                return null;
            var stockResponse = new StockResponse(stockModel.Id, stockModel.ProductId, stockModel.ProductCode, stockModel.AvailableStock, stockModel.StockActionId, stockModel.LastStockOperationDate);
            return stockResponse;
        }
    }
}