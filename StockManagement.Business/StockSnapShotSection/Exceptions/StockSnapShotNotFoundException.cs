using StockManagement.Exceptions;

namespace StockManagement.Business.StockSnapShotSection.Exceptions
{
    public class StockSnapShotNotFoundException : NotFoundException
    {
        public StockSnapShotNotFoundException() : base(string.Format(ExceptionMessages.STOCKSNAPSHOT__NOTFOUND))
        {
        }
        
        public StockSnapShotNotFoundException(long productId) : base(string.Format(ExceptionMessages.STOCKSNAPSHOT__NOTFOUND_X, productId))
        {
        }
    }
}