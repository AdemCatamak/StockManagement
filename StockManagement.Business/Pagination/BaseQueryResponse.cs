using System.Collections.Generic;

namespace StockManagement.Business.Pagination
{
    public abstract class BaseQueryResponse<T>
    {
        public int TotalCount { get; }
        public IReadOnlyList<T> Data { get; }

        protected BaseQueryResponse(int totalCount, IReadOnlyList<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}