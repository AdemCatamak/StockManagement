using System;
using StockManagement.Data.Models.BaseModels;

namespace StockManagement.Data.Models
{
    public class StockSnapShotModel : IEntity<long>, ICreateAudit, IUpdateAudit
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public long ProductId { get; private set; }
        public int AvailableStock { get; private set; }
        public long StockActionId { get; private set; }

        public StockSnapShotModel(long productId, int availableStock, long stockActionId) : this(default, DateTime.UtcNow, DateTime.UtcNow, productId, availableStock, stockActionId)
        {
        }

        public StockSnapShotModel(long id, DateTime createdOn, DateTime updatedOn, long productId, int availableStock, long stockActionId)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ProductId = productId;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
        }

        public void DecreaseStock(int count, long stockActionId)
        {
            AvailableStock -= count;
            StockActionId = stockActionId;
            UpdatedOn = DateTime.UtcNow;
        }

        public void IncreaseStock(int count, long stockActionId)
        {
            AvailableStock += count;
            StockActionId = stockActionId;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}