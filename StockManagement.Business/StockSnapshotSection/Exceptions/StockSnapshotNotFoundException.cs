using StockManagement.Exceptions;

namespace StockManagement.Business.StockSnapshotSection.Exceptions
{
    public class StockSnapshotNotFoundException : NotFoundException
    {
        public StockSnapshotNotFoundException() : base(string.Format(ExceptionMessages.STOCKSNAPSHOT__NOTFOUND))
        {
        }
        
        public StockSnapshotNotFoundException(long productId) : base(string.Format(ExceptionMessages.STOCKSNAPSHOT__NOTFOUND_X, productId))
        {
        }
    }
}