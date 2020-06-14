using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection.Exceptions
{
    public class RemoveFromStockAlreadyExecutedException : ConflictException
    {
        public RemoveFromStockAlreadyExecutedException(string correlationId) : base(string.Format( ExceptionMessages.REMOVEFROMSTOCK__ALREADYEXECUTED, correlationId))
        {
        }
    }
}