using MediatR;
using StockManagement.Business.StockActionSection.Exceptions;
using StockManagement.Business.StockActionSection.Responses;

namespace StockManagement.Business.StockActionSection.Requests
{
    public class RemoveFromStockCommand : IRequest<StockActionResponse>
    {
        public long ProductId { get; }
        public int Count { get; }
        public string CorrelationId { get; }

        public RemoveFromStockCommand(long productId, int count, string correlationId)
        {
            if (count <= 0)
            {
                throw new RemoveFromStockCountNotPositiveException();
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