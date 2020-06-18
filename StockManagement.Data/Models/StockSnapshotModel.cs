using System;
using StockManagement.Data.Models.BaseModels;

namespace StockManagement.Data.Models
{
    public class StockSnapshotModel : IEntity<long>, ICreateAudit, IUpdateAudit
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public long ProductId { get; private set; }
        public int AvailableStock { get; private set; }
        public long StockActionId { get; private set; }
        public DateTime LastStockActionDate { get; private set; }

        public StockSnapshotModel(long productId, int availableStock, long stockActionId, DateTime lastStockActionDate) : this(default, DateTime.UtcNow, DateTime.UtcNow, productId, availableStock, stockActionId, lastStockActionDate)
        {
        }

        public StockSnapshotModel(long id, DateTime createdOn, DateTime updatedOn, long productId, int availableStock, long stockActionId, DateTime lastStockActionDate)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ProductId = productId;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
        }

        public void DecreaseStock(int count, long stockActionId, DateTime lastStockActionDate)
        {
            AvailableStock -= count;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
            UpdatedOn = DateTime.UtcNow;
        }

        public void IncreaseStock(int count, long stockActionId, DateTime lastStockActionDate)
        {
            AvailableStock += count;
            StockActionId = stockActionId;
            LastStockActionDate = lastStockActionDate;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}