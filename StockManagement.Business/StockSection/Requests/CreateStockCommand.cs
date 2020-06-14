using MediatR;
using StockManagement.Business.StockSection.Responses;

namespace StockManagement.Business.StockSection.Requests
{
    public class CreateStockCommand : IRequest<StockResponse>
    {
        public long ProductId { get; }
        public string ProductCode { get; }
        public long StockActionId { get; }

        public CreateStockCommand(long productId, string productCode, long stockActionId)
        {
            ProductId = productId;
            ProductCode = productCode;
            StockActionId = stockActionId;
        }
    }
}