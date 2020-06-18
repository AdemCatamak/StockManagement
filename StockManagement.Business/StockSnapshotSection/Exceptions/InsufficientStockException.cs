using StockManagement.Exceptions;

namespace StockManagement.Business.StockSnapshotSection.Exceptions
{
    public class InsufficientStockException : ValidationException
    {
        public InsufficientStockException(long productId, int availableStock, int requestedCount)
            : base(string.Format(ExceptionMessages.STOCK_INSUFFICENT, productId, availableStock, requestedCount))
        {
        }
    }
}