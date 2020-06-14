using MediatR;
using StockManagement.Business.StockActionSection.Exceptions;
using StockManagement.Business.StockActionSection.Responses;

namespace StockManagement.Business.StockActionSection.Requests
{
    public class AddToStockCommand : IRequest<StockActionResponse>
    {
        public long ProductId { get; }
        public int Count { get; }
        public string CorrelationId { get; }

        public AddToStockCommand(long productId, int count, string correlationId)
        {
            if (count <= 0)
            {
                throw new AddToStockCountNotPositiveException();
            }

            correlationId = correlationId?.Trim() ?? string.Empty;
            if (correlationId == string.Empty)
            {
                throw new CorrelationIdEmptyException();
            }

            ProductId = productId;
            Count = count;
            CorrelationId = correlationId;
        }
    }
}