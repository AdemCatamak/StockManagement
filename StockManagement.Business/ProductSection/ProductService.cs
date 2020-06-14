using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManagement.Business.ProductSection.DomainEvents;
using StockManagement.Business.ProductSection.Exceptions;
using StockManagement.Business.ProductSection.Mappings;
using StockManagement.Business.ProductSection.Requests;
using StockManagement.Business.ProductSection.Responses;
using StockManagement.Data;
using StockManagement.Data.Models;
using StockManagement.Exceptions;

namespace StockManagement.Business.ProductSection
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;

        public ProductService(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new RequestNullException();
            }

            bool productAlreadyExist = _dataContext.ProductModels.Any(p => p.ProductCode == request.ProductCode);
            if (productAlreadyExist)
            {
                throw new ProductAlreadyExistException(request.ProductCode);
            }

            var productModel = new ProductModel(request.ProductCode);

            await _dataContext.ProductModels.AddAsync(productModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var productCreatedEvent = new ProductCreatedEvent(productModel.Id, productModel.ProductCode);
            await _mediator.Publish(productCreatedEvent, cancellationToken);

            ProductResponse productServiceResponse = productModel.ToProductServiceResponse();
            return productServiceResponse;
        }

        public async Task<ProductCollectionResponse> Handle(QueryProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new RequestNullException();
            }

            IQueryable<ProductModel> productModels = _dataContext.ProductModels.AsQueryable();

            if (request.ProductCode != null)
            {
                productModels = productModels.Where(p => p.ProductCode == request.ProductCode);
            }

            if (request.ProductId.HasValue)
            {
                productModels = productModels.Where(p => p.Id == request.ProductId);
            }

            int totalCount = await productModels.CountAsync(cancellationToken: cancellationToken);
            List<ProductModel> productModelList = await productModels.Skip(request.Offset)
                                                                     .Take(request.Take)
                                                                     .ToListAsync(cancellationToken: cancellationToken);

            if (!productModelList.Any())
            {
                throw new ProductNotFoundException();
            }

            List<ProductResponse> productServiceResponseList = productModelList.Select(p => p.ToProductServiceResponse())
                                                                               .ToList();
            var productCollectionServiceResponse = new ProductCollectionResponse(totalCount, productServiceResponseList);
            return productCollectionServiceResponse;
        }
    }
}