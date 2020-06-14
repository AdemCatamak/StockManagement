using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection.Exceptions
{
    public class AddToStockCountNotPositiveException : ValidationException
    {
        public AddToStockCountNotPositiveException() : base(ExceptionMessages.ADDTOSTOCK_COUNT_NOTPOSITIVE)
        {
        }
    }
}