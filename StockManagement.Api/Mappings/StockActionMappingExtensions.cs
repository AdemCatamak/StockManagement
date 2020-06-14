using StockManagement.Api.Contracts.Responses;
using StockManagement.Business.StockActionSection.Responses;

namespace StockManagement.Api.Mappings
{
    public static class StockActionMappingExtensions
    {
        public static StockActionHttpResponse ToStockActionHttpResponse(this StockActionResponse stockActionResponse)
        {
            return stockActionResponse == null
                       ? null
                       : new StockActionHttpResponse
                         {
                             ProductId = stockActionResponse.ProductId,
                             StockActionId = stockActionResponse.StockActionId,
                             StockActionType = stockActionResponse.StockActionType,
                             Count = stockActionResponse.Count
                         };
        }
    }
}