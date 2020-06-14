using StockManagement.Business.StockActionSection.Responses;
using StockManagement.Data.Models;

namespace StockManagement.Business.StockActionSection.Mappings
{
    public static class StockActionMappingExtensions
    {
        public static StockActionResponse ToStockActionServiceResponse(this StockActionModel stockActionModel)
        {
            return stockActionModel == null
                       ? null
                       : new StockActionResponse(stockActionModel.ProductId, stockActionModel.Id, stockActionModel.StockActionType, stockActionModel.Count);
        }
    }
}