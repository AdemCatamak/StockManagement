using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using StockManagement.Business.ProductSection.Requests;
using StockManagement.Business.ProductSection.Responses;
using StockManagement.Business.StockSection.Requests;
using StockManagement.Business.StockSnapshotSection.IntegrationEvent;

namespace StockManagement.Consumers
{
    public class StockCreatorConsumer : IConsumer<StockInitializedIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public StockCreatorConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<StockInitializedIntegrationEvent> context)
        {
            StockInitializedIntegrationEvent stockInitializedIntegrationEvent = context.Message;

            var queryProductCommand = new QueryProductCommand(0, 1)
                                      {
                                          ProductId = stockInitializedIntegrationEvent.ProductId
                                      };
            ProductCollectionResponse productCollectionResponse = await _mediator.Send(queryProductCommand);
            ProductResponse productResponse = productCollectionResponse.Data.First();

            var createStockCommand = new CreateStockCommand(productResponse.ProductId, productResponse.ProductCode, stockInitializedIntegrationEvent.StockActionId, stockInitializedIntegrationEvent.StockCreatedOn);
            await _mediator.Send(createStockCommand);
        }
    }
}