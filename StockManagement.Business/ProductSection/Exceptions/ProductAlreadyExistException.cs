using StockManagement.Exceptions;

namespace StockManagement.Business.ProductSection.Exceptions
{
    public class ProductAlreadyExistException : ConflictException
    {
        public ProductAlreadyExistException(string productCode) : base(string.Format(ExceptionMessages.PRODUCT__ALREADYEXIST, productCode))
        {
        }
    }
}