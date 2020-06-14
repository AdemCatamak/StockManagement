using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection.Exceptions
{
    public class CorrelationIdEmptyException : ValidationException
    {
        public CorrelationIdEmptyException() : base(ExceptionMessages.CORRELATIONID_EMPTY)
        {
        }
    }
}