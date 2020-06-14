using StockManagement.Exceptions;

namespace StockManagement.Business.Pagination.Exceptions
{
    public class TakeInvalidException: ValidationException
    {
        public TakeInvalidException() : base(ExceptionMessages.TAKE_INVALID)
        {
        }
    }
}