using StockManagement.Data.Enum;

namespace StockManagement.Api.Contracts.Responses
{
    public class StockActionHttpResponse
    {
        public long ProductId { get; set; }
        public long StockActionId { get; set; }
        public StockActionTypes StockActionType { get; set; }
        public int Count { get; set; }
    }
}