using System.Collections.Generic;
using StockManagement.Business.Pagination;

namespace StockManagement.Business.ProductSection.Responses
{
    public class ProductCollectionResponse : BaseQueryResponse<ProductResponse>
    {
        public ProductCollectionResponse(int totalCount, IReadOnlyList<ProductResponse> data) : base(totalCount, data)
        {
        }
    }
}