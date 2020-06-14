using StockManagement.Exceptions;

namespace StockManagement.Business.StockSection.Exceptions
{
    public class StockNotFoundException : NotFoundException
    {
        public StockNotFoundException() : base(ExceptionMessages.STOCK__NOTFOUND)
        {
        }
    }
}