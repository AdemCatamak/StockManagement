using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using StockManagement.Business.StockSection.Exceptions;
using StockManagement.Business.StockSection.Requests;
using StockManagement.Business.StockSection.Responses;
using StockManagement.Business.StockSnapShotSection.IntegrationEvent;
using StockManagement.Utility.DistributedLockSection;

namespace StockManagement.Consumers
{
    public class AvailableStockSyncConsumer : IConsumer<StockCountIncreasedIntegrationEvent>,
                                              IConsumer<StockCountDecreasedIntegrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IDistributedLockManager _distributedLockManager;
        private readonly ILogger<AvailableStockSyncConsumer> _logger;


        public AvailableStockSyncConsumer(IMediator mediator, IDistributedLockManager distributedLockManager, ILogger<AvailableStockSyncConsumer> logger)
        {
            _mediator = mediator;
            _distributedLockManager = distributedLockManager;
            _logger = logger;
        }


        public async Task Consume(ConsumeContext<StockCountIncreasedIntegrationEvent> context)
        {
            StockCountIncreasedIntegrationEvent stockCountIncreasedIntegrationEvent = context.Message;
            await SyncAvailableStock(stockCountIncreasedIntegrationEvent.ProductId, stockCountIncreasedIntegrationEvent.StockActionId, stockCountIncreasedIntegrationEvent.AvailableStock);
        }

        public async Task Consume(ConsumeContext<StockCountDecreasedIntegrationEvent> context)
        {
            StockCountDecreasedIntegrationEvent stockCountDecreasedIntegrationEvent = context.Message;
            await SyncAvailableStock(stockCountDecreasedIntegrationEvent.ProductId, stockCountDecreasedIntegrationEvent.StockActionId, stockCountDecreasedIntegrationEvent.AvailableStock);
        }

        private static string UpdateAvailableStockLockKey(long productId) => $"product-{productId}-available-stock-update";

        private async Task SyncAvailableStock(long productId, long stockActionId, int availableStock)
        {
            await _distributedLockManager.LockAsync(UpdateAvailableStockLockKey(productId),
                                                    async () =>
                                                    {
                                                        var queryStockCommand = new QueryStockCommand(0, 1)
                                                                                {
                                                                                    ProductId = productId
                                                                                };

                                                        StockCollectionResponse stockCollectionResponse = await _mediator.Send(queryStockCommand);
                                                        StockResponse stockResponse = stockCollectionResponse.Data.First();

                                                        var updateAvailableStockCommand = new UpdateAvailableStockCommand(stockResponse.StockId, availableStock, stockActionId);
                                                        try
                                                        {
                                                            await _mediator.Send(updateAvailableStockCommand);
                                                        }
                                                        catch (StockTimeLineException e)
                                                        {
                                                            _logger.LogInformation(e.Message);
                                                        }
                                                    });
        }
    }
}