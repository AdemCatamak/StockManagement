using StockManagement.Api.Contracts.Responses;
using StockManagement.Business.ProductSection.Responses;

namespace StockManagement.Api.Mappings
{
    public static class ProductMappingExtensions
    {
        public static ProductHttpResponse ToProductHttpResponse(this ProductResponse productResponse)
        {
            return productResponse == null
                       ? null
                       : new ProductHttpResponse
                         {
                             ProductId = productResponse.ProductId,
                             ProductCode = productResponse.ProductCode,
                         };
        }
    }
}