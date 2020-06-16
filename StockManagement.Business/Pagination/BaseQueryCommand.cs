using StockManagement.Business.Pagination.Exceptions;

namespace StockManagement.Business.Pagination
{
    public abstract class BaseQueryCommand
    {
        public int Offset { get; }
        public int Take { get; }

        protected BaseQueryCommand(int offset, int take)
        {
            if (offset < 0)
                throw new OffsetInvalidException();
            if (take < 1)
                throw new TakeInvalidException();
            
            Offset = offset;
            Take = take;
        }
    }
}