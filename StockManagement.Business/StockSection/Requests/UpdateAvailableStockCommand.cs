using MediatR;
using StockManagement.Business.StockSection.Responses;

namespace StockManagement.Business.StockSection.Requests
{
    public class UpdateAvailableStockCommand : IRequest<StockResponse>
    {
        public long StockId { get; }
        public int AvailableStock { get; }
        public long StockActionId { get; }

        public UpdateAvailableStockCommand(long stockId, int availableStock, long stockActionId)
        {
            StockId = stockId;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
        }
    }
}