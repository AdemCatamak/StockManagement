using StockManagement.Business.ProductSection.Responses;
using StockManagement.Data.Models;

namespace StockManagement.Business.ProductSection.Mappings
{
    public static class ProductMappingExtensions
    {
        public static ProductResponse ToProductServiceResponse(this ProductModel productModel)
        {
            return productModel != null
                       ? new ProductResponse(productModel.Id, productModel.ProductCode)
                       : null;
        }
    }
}