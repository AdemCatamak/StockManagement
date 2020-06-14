using MediatR;
using StockManagement.Business.StockActionSection.Exceptions;
using StockManagement.Business.StockActionSection.Responses;

namespace StockManagement.Business.StockActionSection.Requests
{
    public class ResetStockCommand : IRequest<StockActionResponse>
    {
        public long ProductId { get; }
        public string CorrelationId { get; }

        public ResetStockCommand(long productId, string correlationId)
        {
            correlationId = correlationId?.Trim() ?? string.Empty;
            if (correlationId == string.Empty)
            {
                throw new CorrelationIdEmptyException();
            }

            ProductId = productId;
            CorrelationId = correlationId;
        }
    }
}