using System.Collections.Generic;
using StockManagement.Business.Pagination;

namespace StockManagement.Business.StockSection.Responses
{
    public class StockCollectionResponse : BaseQueryResponse<StockResponse>
    {
        public StockCollectionResponse(int totalCount, IReadOnlyList<StockResponse> data) : base(totalCount, data)
        {
        }
    }
}