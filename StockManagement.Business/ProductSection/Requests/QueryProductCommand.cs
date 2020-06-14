using MediatR;
using StockManagement.Business.Pagination;
using StockManagement.Business.ProductSection.Responses;

namespace StockManagement.Business.ProductSection.Requests
{
    public class QueryProductCommand : BaseQueryCommand, IRequest<ProductCollectionResponse>
    {
        public string ProductCode { get; set; }
        public long? ProductId { get; set; }

        public QueryProductCommand(int offset, int take) : base(offset, take)
        {
        }
    }
}