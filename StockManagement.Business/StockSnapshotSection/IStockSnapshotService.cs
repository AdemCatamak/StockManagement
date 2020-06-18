using MediatR;
using StockManagement.Business.StockActionSection.DomainEvents;
using StockManagement.Business.StockSnapshotSection.Requests;
using StockManagement.Business.StockSnapshotSection.Responses;

namespace StockManagement.Business.StockSnapshotSection
{
    public interface IStockSnapshotService : IBusinessService,
                                             INotificationHandler<StockInitializedEvent>,
                                             INotificationHandler<StockCountIncreasedEvent>,
                                             INotificationHandler<StockCountDecreasedEvent>,
                                             IRequestHandler<QueryStockSnapshotCommand, StockSnapshotCollectionResponse>
    {
    }
}