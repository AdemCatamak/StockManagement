using MediatR;
using StockManagement.Business.StockActionSection.DomainEvents;
using StockManagement.Business.StockSnapShotSection.Requests;
using StockManagement.Business.StockSnapShotSection.Responses;

namespace StockManagement.Business.StockSnapShotSection
{
    public interface IStockSnapShotService : IBusinessService,
                                             INotificationHandler<StockInitializedEvent>,
                                             INotificationHandler<StockCountIncreasedEvent>,
                                             INotificationHandler<StockCountDecreasedEvent>,
                                             INotificationHandler<StockCountSetEvent>,
                                             IRequestHandler<QueryStockSnapShotCommand, StockSnapShotCollectionResponse>
    {
    }
}