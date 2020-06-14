using System.Collections.Generic;

namespace StockManagement.Business.Pagination
{
    public abstract class BaseQueryResponse<T>
    {
        public int TotalCount { get; private set; }
        public IReadOnlyList<T> Data { get; private set; }

        protected BaseQueryResponse(int totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}