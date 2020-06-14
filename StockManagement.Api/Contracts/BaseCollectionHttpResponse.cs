using System.Collections.Generic;

namespace StockManagement.Api.Contracts
{
    public abstract class BaseCollectionHttpResponse<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}