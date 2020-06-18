using MediatR;
using StockManagement.Business.Pagination;
using StockManagement.Business.StockSnapshotSection.Responses;

namespace StockManagement.Business.StockSnapshotSection.Requests
{
    public class QueryStockSnapshotCommand : BaseQueryCommand,
                                             IRequest<StockSnapshotCollectionResponse>
    {
        public long? ProductId { get; set; }

        public QueryStockSnapshotCommand(int offset, int take) : base(offset, take)
        {
        }
    }
}