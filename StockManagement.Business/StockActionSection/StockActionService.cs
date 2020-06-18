using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StockManagement.Business.ProductSection.DomainEvents;
using StockManagement.Business.StockActionSection.DomainEvents;
using StockManagement.Business.StockActionSection.Exceptions;
using StockManagement.Business.StockActionSection.Mappings;
using StockManagement.Business.StockActionSection.Requests;
using StockManagement.Business.StockActionSection.Responses;
using StockManagement.Business.StockSnapshotSection.Requests;
using StockManagement.Business.StockSnapshotSection.Responses;
using StockManagement.Data;
using StockManagement.Data.Enum;
using StockManagement.Data.Models;
using StockManagement.Exceptions;

namespace StockManagement.Business.StockActionSection
{
    public class StockActionService : IStockActionService
    {
        private readonly DataContext _dataContext;
        private readonly IMediator _mediator;

        public StockActionService(DataContext dataContext, IMediator mediator)
        {
            _dataContext = dataContext;
            _mediator = mediator;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var stockActionModel = new StockActionModel(notification.ProductId, StockActionTypes.InitializeStock, 0, $"{notification.ProductId}-{StockActionTypes.InitializeStock}");

            await _dataContext.StockActionModels.AddAsync(stockActionModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockInitializedEvent = new StockInitializedEvent(stockActionModel.ProductId, stockActionModel.Id, stockActionModel.CreatedOn);
            await _mediator.Publish(stockInitializedEvent, cancellationToken);
        }

        public async Task<StockActionResponse> Handle(AddToStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestNullException();

            bool addToStockAlreadyExecuted = await _dataContext.StockActionModels
                                                               .AnyAsync(s => s.CorrelationId == request.CorrelationId
                                                                           && s.StockActionType == StockActionTypes.AddToStock,
                                                                         cancellationToken);

            if (addToStockAlreadyExecuted)
            {
                throw new AddToStockAlreadyExecutedException(request.CorrelationId);
            }

            var stockActionModel = new StockActionModel(request.ProductId, StockActionTypes.AddToStock, request.Count, request.CorrelationId);

            await _dataContext.StockActionModels.AddAsync(stockActionModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockIncreasedEvent = new StockCountIncreasedEvent(stockActionModel.ProductId, stockActionModel.Id, stockActionModel.Count, stockActionModel.CreatedOn);
            await _mediator.Publish(stockIncreasedEvent, cancellationToken);

            StockActionResponse stockActionServiceResponse = stockActionModel.ToStockActionServiceResponse();
            return stockActionServiceResponse;
        }

        public async Task<StockActionResponse> Handle(RemoveFromStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestNullException();

            bool allocationAlreadyExecuted = await _dataContext.StockActionModels
                                                               .AnyAsync(s => s.CorrelationId == request.CorrelationId
                                                                           && s.StockActionType == StockActionTypes.RemoveFromStock
                                                                       , cancellationToken);

            if (allocationAlreadyExecuted)
            {
                throw new RemoveFromStockAlreadyExecutedException(request.CorrelationId);
            }

            var stockActionModel = new StockActionModel(request.ProductId, StockActionTypes.RemoveFromStock, request.Count, request.CorrelationId);

            await _dataContext.StockActionModels.AddAsync(stockActionModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockInitializedEvent = new StockCountDecreasedEvent(stockActionModel.ProductId, stockActionModel.Id, stockActionModel.Count, stockActionModel.CreatedOn);
            await _mediator.Publish(stockInitializedEvent, cancellationToken);

            StockActionResponse stockActionServiceResponse = stockActionModel.ToStockActionServiceResponse();
            return stockActionServiceResponse;
        }

        public async Task<StockActionResponse> Handle(ResetStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestNullException();

            var queryStockSnapshotCommand = new QueryStockSnapshotCommand(0, 1)
                                            {
                                                ProductId = request.ProductId
                                            };
            StockSnapshotCollectionResponse stockSnapshotCollectionResponse = await _mediator.Send(queryStockSnapshotCommand, cancellationToken);
            StockSnapshotResponse stockSnapshotResponse = stockSnapshotCollectionResponse.Data.First();
            
            var stockActionModel = new StockActionModel(request.ProductId, StockActionTypes.ResetStock, stockSnapshotResponse.AvailableStock, request.CorrelationId);

            await _dataContext.StockActionModels.AddAsync(stockActionModel, cancellationToken);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockCountSetEvent = new StockCountDecreasedEvent(stockActionModel.ProductId, stockActionModel.Id, stockActionModel.Count, stockActionModel.CreatedOn);
            await _mediator.Publish(stockCountSetEvent, cancellationToken);

            StockActionResponse stockActionServiceResponse = stockActionModel.ToStockActionServiceResponse();
            return stockActionServiceResponse;
        }
    }
}