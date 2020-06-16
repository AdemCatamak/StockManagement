using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Contracts.Responses;
using StockManagement.Api.Contracts.StockRequests;
using StockManagement.Api.Mappings;
using StockManagement.Business.ProductSection.Requests;
using StockManagement.Business.StockActionSection.Requests;
using StockManagement.Business.StockActionSection.Responses;
using StockManagement.Business.StockSection.Requests;
using StockManagement.Business.StockSection.Responses;
using StockManagement.Utility.DistributedLockSection;

namespace StockManagement.Api.Controllers
{
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedLockManager _distributedLockManager;

        public StockController(IMediator mediator, IDistributedLockManager distributedLockManager)
        {
            _mediator = mediator;
            _distributedLockManager = distributedLockManager;
        }

        private string StockOperationLockKey(long productId) => $"stock-operation-for-product-{productId}";

        [ProducesResponseType(typeof(StockCollectionHttpResponse), (int) HttpStatusCode.OK)]
        [HttpGet("stocks")]
        public async Task<IActionResult> GetStock([FromQuery] GetStockCollectionHttpRequest getStockCollectionHttpRequest)
        {
            var queryStockCommand = new QueryStockCommand(getStockCollectionHttpRequest?.Offset ?? 0,
                                                          getStockCollectionHttpRequest?.Take ?? 1)
                                    {
                                        StockId = getStockCollectionHttpRequest?.StockId,
                                        ProductId = getStockCollectionHttpRequest?.ProductId,
                                        PartialProductCode = getStockCollectionHttpRequest?.ProductCode,
                                        StockUpdatedLaterThan = getStockCollectionHttpRequest?.StockUpdatedLaterThan
                                    };
            StockCollectionResponse stockCollectionResponse = await _mediator.Send(queryStockCommand);

            var stockCollectionHttpResponse = new StockCollectionHttpResponse
                                              {
                                                  TotalCount = stockCollectionResponse.TotalCount,
                                                  Data = stockCollectionResponse.Data
                                                                                .Select(s => s.ToStockHttpResponse())
                                                                                .ToList()
                                              };
            return StatusCode((int) HttpStatusCode.OK, stockCollectionHttpResponse);
        }

        [ProducesResponseType(typeof(StockActionHttpResponse), (int) HttpStatusCode.OK)]
        [HttpPost("products/{productId}/add-to-stock")]
        public async Task<IActionResult> AddToStock([FromRoute] long productId, [FromBody] AddToStockHttpRequest addToStockHttpRequest)
        {
            var queryProductServiceRequest = new QueryProductCommand(0, 1)
                                             {
                                                 ProductId = productId
                                             };

            StockActionHttpResponse stockActionHttpResponse = null;
            await _distributedLockManager.LockAsync(StockOperationLockKey(productId),
                                                    async () =>
                                                    {
                                                        await _mediator.Send(queryProductServiceRequest);

                                                        AddToStockCommand addToStockCommand = addToStockHttpRequest != null
                                                                                                  ? new AddToStockCommand(productId, addToStockHttpRequest.Count, addToStockHttpRequest.CorrelationId)
                                                                                                  : null;

                                                        StockActionResponse stockActionResponse = await _mediator.Send(addToStockCommand);

                                                        stockActionHttpResponse = stockActionResponse.ToStockActionHttpResponse();
                                                    });


            return StatusCode((int) HttpStatusCode.OK, stockActionHttpResponse);
        }

        [ProducesResponseType(typeof(StockActionHttpResponse), (int) HttpStatusCode.OK)]
        [HttpPost("products/{productId}/remove-from-stock")]
        public async Task<IActionResult> RemoveFromStock([FromRoute] long productId, [FromBody] RemoveFromStockHttpRequest removeFromStockHttpRequest)
        {
            var queryProductServiceRequest = new QueryProductCommand(0, 1)
                                             {
                                                 ProductId = productId
                                             };

            await _mediator.Send(queryProductServiceRequest);

            StockActionHttpResponse stockActionHttpResponse = null;
            await _distributedLockManager.LockAsync(StockOperationLockKey(productId),
                                                    async () =>
                                                    {
                                                        RemoveFromStockCommand removeFromStockCommand = removeFromStockHttpRequest != null
                                                                                                            ? new RemoveFromStockCommand(productId, removeFromStockHttpRequest.Count, removeFromStockHttpRequest.CorrelationId)
                                                                                                            : null;

                                                        StockActionResponse stockActionResponse = await _mediator.Send(removeFromStockCommand);

                                                        stockActionHttpResponse = stockActionResponse.ToStockActionHttpResponse();
                                                    });

            return StatusCode((int) HttpStatusCode.OK, stockActionHttpResponse);
        }

        [ProducesResponseType(typeof(StockActionHttpResponse), (int) HttpStatusCode.OK)]
        [HttpPost("products/{productId}/reset-stock")]
        public async Task<IActionResult> ResetStock([FromRoute] long productId, [FromBody] ResetStockHttpRequest resetStockHttpRequest)
        {
            var queryProductServiceRequest = new QueryProductCommand(0, 1)
                                             {
                                                 ProductId = productId
                                             };

            await _mediator.Send(queryProductServiceRequest);

            StockActionHttpResponse stockActionHttpResponse = null;
            await _distributedLockManager.LockAsync(StockOperationLockKey(productId),
                                                    async () =>
                                                    {
                                                        ResetStockCommand resetStockCommand = resetStockHttpRequest != null
                                                                                                  ? new ResetStockCommand(productId, resetStockHttpRequest.CorrelationId)
                                                                                                  : null;

                                                        StockActionResponse stockActionResponse = await _mediator.Send(resetStockCommand);
                                                        stockActionHttpResponse = stockActionResponse.ToStockActionHttpResponse();
                                                    });

            return StatusCode((int) HttpStatusCode.OK, stockActionHttpResponse);
        }
    }
}