using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection.Exceptions
{
    public class AddToStockAlreadyExecutedException : ConflictException
    {
        public AddToStockAlreadyExecutedException(string correlationId) : base(string.Format(ExceptionMessages.ADDTOSTOCK__ALREADYEXECUTED, correlationId))
        {
        }
    }
}