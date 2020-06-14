using StockManagement.Exceptions;

namespace StockManagement.Business.Pagination.Exceptions
{
    public class OffsetInvalidException : ValidationException
    {
        public OffsetInvalidException() : base(ExceptionMessages.OFFSET_INVALID)
        {
        }
    }
}