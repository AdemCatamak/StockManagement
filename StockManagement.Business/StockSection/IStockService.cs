using MediatR;
using StockManagement.Business.StockSection.Requests;
using StockManagement.Business.StockSection.Responses;

namespace StockManagement.Business.StockSection
{
    public interface IStockService :
        IRequestHandler<CreateStockCommand, StockResponse>,
        IRequestHandler<UpdateAvailableStockCommand, StockResponse>,
        IRequestHandler<QueryStockCommand, StockCollectionResponse>
    {
    }
}