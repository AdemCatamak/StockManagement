using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection.Exceptions
{
    public class RemoveFromStockCountNotPositiveException : ValidationException
    {
        public RemoveFromStockCountNotPositiveException() : base(ExceptionMessages.REMOVEFROMSTOCK_COUNT_NOTPOSITIVE)
        {
        }
    }
}