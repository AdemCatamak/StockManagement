using System;
using MediatR;
using StockManagement.Business.Pagination;
using StockManagement.Business.StockSection.Responses;

namespace StockManagement.Business.StockSection.Requests
{
    public class QueryStockCommand : BaseQueryCommand
                                   , IRequest<StockCollectionResponse>
    {
        public long? StockId { get; set; }
        public long? ProductId { get; set; }
        public string PartialProductCode { get; set; }
        public DateTime? StockUpdatedLaterThan { get; set; }

        public QueryStockCommand(int offset, int take) : base(offset, take)
        {
        }
    }
}