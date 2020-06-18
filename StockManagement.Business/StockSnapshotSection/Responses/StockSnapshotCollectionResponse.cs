using System.Collections.Generic;
using StockManagement.Business.Pagination;

namespace StockManagement.Business.StockSnapshotSection.Responses
{
    public class StockSnapshotCollectionResponse : BaseQueryResponse<StockSnapshotResponse>
    {
        public StockSnapshotCollectionResponse(int totalCount, IReadOnlyList<StockSnapshotResponse> data) : base(totalCount, data)
        {
        }
    }
}