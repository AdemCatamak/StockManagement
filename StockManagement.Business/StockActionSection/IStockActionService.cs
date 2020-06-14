using MediatR;
using StockManagement.Business.ProductSection.DomainEvents;
using StockManagement.Business.StockActionSection.Requests;
using StockManagement.Business.StockActionSection.Responses;

namespace StockManagement.Business.StockActionSection
{
    public interface IStockActionService : IBusinessService,
                                           INotificationHandler<ProductCreatedEvent>,
                                           IRequestHandler<AddToStockCommand, StockActionResponse>,
                                           IRequestHandler<RemoveFromStockCommand, StockActionResponse>,
                                           IRequestHandler<ResetStockCommand, StockActionResponse>
    {
    }
}