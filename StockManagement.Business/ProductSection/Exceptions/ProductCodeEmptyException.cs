using StockManagement.Exceptions;

namespace StockManagement.Business.ProductSection.Exceptions
{
    public class ProductCodeEmptyException : ValidationException
    {
        public ProductCodeEmptyException() : base(ExceptionMessages.PRODUCT_CODE_EMPTY)
        {
        }
    }
}