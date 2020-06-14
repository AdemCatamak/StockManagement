using MediatR;
using StockManagement.Business.Pagination;
using StockManagement.Business.StockSnapShotSection.Responses;

namespace StockManagement.Business.StockSnapShotSection.Requests
{
    public class QueryStockSnapShotCommand : BaseQueryCommand,
                                             IRequest<StockSnapShotCollectionResponse>
    {
        public long? ProductId { get; set; }

        public QueryStockSnapShotCommand(int offset, int take) : base(offset, take)
        {
        }
    }
}