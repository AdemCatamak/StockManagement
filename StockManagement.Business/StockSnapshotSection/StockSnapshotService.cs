using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockManagement.Business.StockActionSection.DomainEvents;
using StockManagement.Business.StockSnapshotSection.Exceptions;
using StockManagement.Business.StockSnapshotSection.IntegrationEvent;
using StockManagement.Business.StockSnapshotSection.Mappings;
using StockManagement.Business.StockSnapshotSection.Requests;
using StockManagement.Business.StockSnapshotSection.Responses;
using StockManagement.Data;
using StockManagement.Data.Models;
using StockManagement.Exceptions;
using StockManagement.Utility.IntegrationEventPublisherSection;

namespace StockManagement.Business.StockSnapshotSection
{
    public class StockSnapshotService : IStockSnapshotService
    {
        private readonly DataContext _dataContext;
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        public StockSnapshotService(DataContext dataContext, IIntegrationEventPublisher integrationEventPublisher)
        {
            _dataContext = dataContext;
            _integrationEventPublisher = integrationEventPublisher;
        }

        public async Task Handle(StockInitializedEvent notification, CancellationToken cancellationToken)
        {
            var stockSnapshotModel = new StockSnapshotModel(notification.ProductId, 0, notification.StockActionId, notification.StockActionDate);
            await _dataContext.StockSnapshotModels.AddAsync(stockSnapshotModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockSnapshotCreatedIntegrationEvent = new StockSnapshotCreatedIntegrationEvent(stockSnapshotModel.ProductId, stockSnapshotModel.Id, stockSnapshotModel.StockActionId, stockSnapshotModel.LastStockActionDate);
            _integrationEventPublisher.AddEvent(stockSnapshotCreatedIntegrationEvent);
        }

        public async Task Handle(StockCountIncreasedEvent notification, CancellationToken cancellationToken)
        {
            await IncreaseStock(notification.ProductId, notification.Count, notification.StockActionId, notification.StockActionDate, cancellationToken);
        }

        public async Task Handle(StockCountDecreasedEvent notification, CancellationToken cancellationToken)
        {
            await DecreaseStock(notification.ProductId, notification.Count, notification.StockActionId, notification.StockActionDate, cancellationToken);
        }

        public async Task Handle(StockCountSetEvent notification, CancellationToken cancellationToken)
        {
            StockSnapshotModel stockSnapshotModel = await _dataContext.StockSnapshotModels.FirstAsync(s => s.ProductId == notification.ProductId, cancellationToken: cancellationToken);
            stockSnapshotModel.DecreaseStock(stockSnapshotModel.AvailableStock, notification.StockActionId, notification.StockActionDate);

            await _dataContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<StockSnapshotCollectionResponse> Handle(QueryStockSnapshotCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestNullException();

            IQueryable<StockSnapshotModel> stockSnapshotModels = _dataContext.StockSnapshotModels.AsQueryable();

            if (request.ProductId.HasValue)
                stockSnapshotModels = stockSnapshotModels.Where(s => s.ProductId == request.ProductId);

            int totalCount = await stockSnapshotModels.CountAsync(cancellationToken);
            List<StockSnapshotModel> snapshotModelList = await stockSnapshotModels.Skip(request.Offset)
                                                                                  .Take(request.Take)
                                                                                  .ToListAsync(cancellationToken);

            if (!snapshotModelList.Any())
            {
                throw new StockSnapshotNotFoundException();
            }

            List<StockSnapshotResponse> stockServiceResponseList = snapshotModelList.Select(s => s.ToStockServiceResponse())
                                                                                    .ToList();

            return new StockSnapshotCollectionResponse(totalCount, stockServiceResponseList);
        }

        private async Task IncreaseStock(long productId, int count, long stockActionId, DateTime stockActionDate, CancellationToken cancellationToken)
        {
            StockSnapshotModel stockSnapshotModel = await _dataContext.StockSnapshotModels
                                                                      .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

            if (stockSnapshotModel == null)
            {
                throw new StockSnapshotNotFoundException(productId);
            }

            stockSnapshotModel.IncreaseStock(count, stockActionId, stockActionDate);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockCountIncreasedIntegrationEvent = new StockCountIncreasedIntegrationEvent(stockSnapshotModel.ProductId, stockSnapshotModel.StockActionId, count, stockSnapshotModel.AvailableStock, stockActionDate);
            _integrationEventPublisher.AddEvent(stockCountIncreasedIntegrationEvent);
        }

        private async Task DecreaseStock(long productId, int count, long stockActionId, DateTime stockActionDate, CancellationToken cancellationToken)
        {
            StockSnapshotModel stockSnapshotModel = await _dataContext.StockSnapshotModels
                                                                      .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

            if (stockSnapshotModel == null)
            {
                throw new StockSnapshotNotFoundException(productId);
            }

            if (stockSnapshotModel.AvailableStock < count)
            {
                throw new InsufficientStockException(productId, stockSnapshotModel.AvailableStock, count);
            }

            stockSnapshotModel.DecreaseStock(count, stockActionId, stockActionDate);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockCountDecreasedIntegrationEvent = new StockCountDecreasedIntegrationEvent(stockSnapshotModel.ProductId, stockSnapshotModel.StockActionId, count, stockSnapshotModel.AvailableStock, stockSnapshotModel.LastStockActionDate);
            _integrationEventPublisher.AddEvent(stockCountDecreasedIntegrationEvent);
        }
    }
}