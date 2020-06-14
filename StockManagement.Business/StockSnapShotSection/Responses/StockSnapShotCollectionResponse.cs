using System.Collections.Generic;
using StockManagement.Business.Pagination;

namespace StockManagement.Business.StockSnapShotSection.Responses
{
    public class StockSnapShotCollectionResponse : BaseQueryResponse<StockSnapshotResponse>
    {
        public StockSnapShotCollectionResponse(int totalCount, IReadOnlyList<StockSnapshotResponse> data) : base(totalCount, data)
        {
        }
    }
}