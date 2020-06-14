using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Business.StockActionSection.DomainEvents;
using StockManagement.Business.StockSnapShotSection.Exceptions;
using StockManagement.Business.StockSnapShotSection.IntegrationEvent;
using StockManagement.Business.StockSnapShotSection.Mappings;
using StockManagement.Business.StockSnapShotSection.Requests;
using StockManagement.Business.StockSnapShotSection.Responses;
using StockManagement.Data;
using StockManagement.Data.Models;
using StockManagement.Exceptions;
using StockManagement.Utility.IntegrationEventHandlerSection;

namespace StockManagement.Business.StockSnapShotSection
{
    public class StockSnapShotService : IStockSnapShotService
    {
        private readonly DataContext _dataContext;
        private readonly IIntegrationEventHandler _integrationEventHandler;

        public StockSnapShotService(DataContext dataContext, IIntegrationEventHandler integrationEventHandler)
        {
            _dataContext = dataContext;
            _integrationEventHandler = integrationEventHandler;
        }

        public async Task Handle(StockInitializedEvent notification, CancellationToken cancellationToken)
        {
            var stockSnapShotModel = new StockSnapShotModel(notification.ProductId, 0, notification.StockActionId);
            await _dataContext.StockSnapShotModels.AddAsync(stockSnapShotModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockSnapShotCreatedIntegrationEvent = new StockSnapShotCreatedIntegrationEvent(stockSnapShotModel.ProductId, stockSnapShotModel.Id, stockSnapShotModel.StockActionId);
            _integrationEventHandler.AddEvent(stockSnapShotCreatedIntegrationEvent);
        }

        public async Task Handle(StockCountIncreasedEvent notification, CancellationToken cancellationToken)
        {
            await IncreaseStock(notification.ProductId, notification.Count, notification.StockActionId, cancellationToken);
        }

        public async Task Handle(StockCountDecreasedEvent notification, CancellationToken cancellationToken)
        {
            await DecreaseStock(notification.ProductId, notification.Count, notification.StockActionId, cancellationToken);
        }

        public async Task Handle(StockCountSetEvent notification, CancellationToken cancellationToken)
        {
            StockSnapShotModel stockSnapShotModel = await _dataContext.StockSnapShotModels.FirstAsync(s => s.ProductId == notification.ProductId, cancellationToken: cancellationToken);
            stockSnapShotModel.DecreaseStock(stockSnapShotModel.AvailableStock, notification.StockActionId);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<StockSnapShotCollectionResponse> Handle(QueryStockSnapShotCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestNullException();

            IQueryable<StockSnapShotModel> stockSnapShotModels = _dataContext.StockSnapShotModels.AsQueryable();

            if (request.ProductId.HasValue)
                stockSnapShotModels = stockSnapShotModels.Where(s => s.ProductId == request.ProductId);

            int totalCount = await stockSnapShotModels.CountAsync(cancellationToken);
            List<StockSnapShotModel> snapShotModelList = await stockSnapShotModels.Skip(request.Offset)
                                                                                  .Take(request.Take)
                                                                                  .ToListAsync(cancellationToken);

            if (!snapShotModelList.Any())
            {
                throw new StockSnapShotNotFoundException();
            }

            List<StockSnapshotResponse> stockServiceResponseList = snapShotModelList.Select(s => s.ToStockServiceResponse())
                                                                                    .ToList();

            return new StockSnapShotCollectionResponse(totalCount, stockServiceResponseList);
        }

        private async Task DecreaseStock(long productId, int count, long stockActionId, CancellationToken cancellationToken)
        {
            StockSnapShotModel stockSnapShotModel = await _dataContext.StockSnapShotModels
                                                                      .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

            if (stockSnapShotModel == null)
            {
                throw new StockSnapShotNotFoundException(productId);
            }

            if (stockSnapShotModel.AvailableStock < count)
            {
                throw new InsufficientStockException(productId, stockSnapShotModel.AvailableStock, count);
            }

            stockSnapShotModel.DecreaseStock(count, stockActionId);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockCountDecreasedIntegrationEvent = new StockCountDecreasedIntegrationEvent(stockSnapShotModel.ProductId, stockSnapShotModel.StockActionId, count, stockSnapShotModel.AvailableStock);
            _integrationEventHandler.AddEvent(stockCountDecreasedIntegrationEvent);
        }

        private async Task IncreaseStock(long productId, int count, long stockActionId, CancellationToken cancellationToken)
        {
            StockSnapShotModel stockSnapShotModel = await _dataContext.StockSnapShotModels
                                                                      .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

            if (stockSnapShotModel == null)
            {
                throw new StockSnapShotNotFoundException(productId);
            }

            stockSnapShotModel.IncreaseStock(count, stockActionId);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockCountIncreasedIntegrationEvent = new StockCountIncreasedIntegrationEvent(stockSnapShotModel.ProductId, stockSnapShotModel.StockActionId, count, stockSnapShotModel.AvailableStock);
            _integrationEventHandler.AddEvent(stockCountIncreasedIntegrationEvent);
        }
    }
}