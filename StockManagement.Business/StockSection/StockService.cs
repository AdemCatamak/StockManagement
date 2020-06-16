using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using StockManagement.Business.StockSection.Exceptions;
using StockManagement.Business.StockSection.Mappings;
using StockManagement.Business.StockSection.Requests;
using StockManagement.Business.StockSection.Responses;
using StockManagement.Data;
using StockManagement.Data.Models;

namespace StockManagement.Business.StockSection
{
    public class StockService : IStockService
    {
        private readonly DataContext _dataContext;

        public StockService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<StockResponse> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestException();

            var stockModel = new StockModel(request.ProductId, request.ProductCode, 0, request.StockActionId, request.LastStockOperationDate);
            _dataContext.Add(stockModel);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockResponse = stockModel.ToStockResponse();
            return stockResponse;
        }

        public async Task<StockResponse> Handle(UpdateAvailableStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestException();

            StockModel stockModel = _dataContext.StockModels.First(s => s.Id == request.StockId);

            if (stockModel.StockActionId > request.StockActionId)
            {
                throw new StockTimeLineException(stockModel.StockActionId, request.StockActionId);
            }

            stockModel.UpdateStock(request.AvailableStock, request.StockActionId, request.LastStockOperationDate);
            await _dataContext.SaveChangesAsync(cancellationToken);

            var stockResponse = stockModel.ToStockResponse();
            return stockResponse;
        }

        public async Task<StockCollectionResponse> Handle(QueryStockCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new RequestException();

            IQueryable<StockModel> stockModels = _dataContext.StockModels.AsQueryable();

            if (request.StockId.HasValue)
                stockModels = stockModels.Where(s => s.Id == request.StockId);

            if (request.ProductId.HasValue)
                stockModels = stockModels.Where(s => s.ProductId == request.ProductId);

            if (request.PartialProductCode != null)
                stockModels = stockModels.Where(s => s.ProductCode.Contains(request.PartialProductCode));

            if (request.StockUpdatedLaterThan.HasValue)
                stockModels = stockModels.Where(s => s.LastStockOperationDate > request.StockUpdatedLaterThan);

            int totalCount = await stockModels.CountAsync(cancellationToken: cancellationToken);
            List<StockModel> stockModelList = await stockModels.Skip(request.Offset)
                                                               .Take(request.Take)
                                                               .ToListAsync(cancellationToken: cancellationToken);

            if (!stockModelList.Any())
            {
                throw new StockNotFoundException();
            }

            List<StockResponse> stockResponseList = stockModelList.Select(s => s.ToStockResponse())
                                                                  .ToList();

            return new StockCollectionResponse(totalCount, stockResponseList);
        }
    }
}