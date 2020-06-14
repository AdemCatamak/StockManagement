using StockManagement.Exceptions;

namespace StockManagement.Business.ProductSection.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException() : base(ExceptionMessages.PRODUCT__NOTFOUND)
        {
        }
    }
}