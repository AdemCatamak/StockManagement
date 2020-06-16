using MediatR;
using StockManagement.Business.ProductSection.Exceptions;
using StockManagement.Business.ProductSection.Responses;

namespace StockManagement.Business.ProductSection.Requests
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public string ProductCode { get; }

        public CreateProductCommand(string productCode)
        {
            productCode = productCode?.Trim() ?? string.Empty;
            if (productCode == string.Empty)
                throw new ProductCodeEmptyException();

            ProductCode = productCode;
        }
    }
}