using StockManagement.Api.Contracts.Responses;
using StockManagement.Business.StockSection.Responses;

namespace StockManagement.Api.Mappings
{
    public static class StockMappingExtension
    {
        public static StockHttpResponse ToStockHttpResponse(this StockResponse stockResponse)
        {
            return stockResponse == null
                       ? null
                       : new StockHttpResponse
                         {
                             StockId = stockResponse.StockId,
                             ProductId = stockResponse.ProductId,
                             ProductCode = stockResponse.ProductCode,
                             AvailableStock = stockResponse.AvailableStock,
                             LastStockOperationDate = stockResponse.LastStockOperationDate
                         };
        }
    }
}