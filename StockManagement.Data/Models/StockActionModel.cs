using System;
using StockManagement.Data.Enum;
using StockManagement.Data.Models.BaseModels;

namespace StockManagement.Data.Models
{
    public class StockActionModel : IEntity<long>, ICreateAudit
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public long ProductId { get; private set; }
        public StockActionTypes StockActionType { get; private set; }
        public int Count { get; private set; }
        public string CorrelationId { get; private set; }

        public StockActionModel(long productId, StockActionTypes stockActionType, int count, string correlationId) : this(default, DateTime.UtcNow, productId, stockActionType, count, correlationId)
        {
        }

        public StockActionModel(long id, DateTime createdOn, long productId, StockActionTypes stockActionType, int count, string correlationId)
        {
            Id = id;
            CreatedOn = createdOn;
            ProductId = productId;
            StockActionType = stockActionType;
            Count = count;
            CorrelationId = correlationId;
        }
    }
}