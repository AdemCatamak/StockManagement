using StockManagement.Data.Enum;

namespace StockManagement.Business.StockActionSection.Responses
{
    public class StockActionResponse
    {
        public long ProductId { get; }
        public long StockActionId { get; }
        public StockActionTypes StockActionType { get; }
        public int Count { get; }

        public StockActionResponse(long productId, long stockActionId, StockActionTypes stockActionType, int count)
        {
            StockActionId = stockActionId;
            StockActionType = stockActionType;
            Count = count;
            ProductId = productId;
        }
    }
}