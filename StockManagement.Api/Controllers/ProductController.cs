using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Contracts.ProductRequests;
using StockManagement.Api.Contracts.Responses;
using StockManagement.Api.Mappings;
using StockManagement.Business.ProductSection.Requests;
using StockManagement.Business.ProductSection.Responses;

namespace StockManagement.Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("products")]
        [ProducesResponseType(typeof(ProductHttpResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> PostProduct([FromBody] PostProductHttpRequest postProductHttpRequest)
        {
            CreateProductCommand createProductCommand
                = postProductHttpRequest != null
                      ? new CreateProductCommand(postProductHttpRequest.ProductCode)
                      : null;

            ProductResponse productResponse = await _mediator.Send(createProductCommand);

            var productHttpResponse = productResponse.ToProductHttpResponse();

            return StatusCode((int) HttpStatusCode.OK, productHttpResponse);
        }

        [HttpGet("products")]
        [ProducesResponseType(typeof(ProductCollectionHttpResponse), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductCollectionHttpRequest getProductCollectionHttpRequest)
        {
            QueryProductCommand queryProductCommand
                = getProductCollectionHttpRequest != null
                      ? new QueryProductCommand(getProductCollectionHttpRequest.Offset, getProductCollectionHttpRequest.Take)
                        {
                            ProductCode = getProductCollectionHttpRequest.ProductCode
                        }
                      : null;

            ProductCollectionResponse productCollectionResponse = await _mediator.Send(queryProductCommand);

            var productCollectionHttpResponse = new ProductCollectionHttpResponse
                                                {
                                                    TotalCount = productCollectionResponse.TotalCount,
                                                    Data = productCollectionResponse.Data
                                                                                           .Select(d => d.ToProductHttpResponse())
                                                                                           .ToList()
                                                };
            return StatusCode((int) HttpStatusCode.OK, productCollectionHttpResponse);
        }
    }
}