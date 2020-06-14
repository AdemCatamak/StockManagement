using MediatR;
using StockManagement.Business.ProductSection.Requests;
using StockManagement.Business.ProductSection.Responses;

namespace StockManagement.Business.ProductSection
{
    public interface IProductService : IBusinessService,
                                       IRequestHandler<CreateProductCommand, ProductResponse>,
                                       IRequestHandler<QueryProductCommand, ProductCollectionResponse>
    {
    }
}