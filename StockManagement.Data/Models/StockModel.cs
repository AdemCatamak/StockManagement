using System;
using StockManagement.Data.Models.BaseModels;

namespace StockManagement.Data.Models
{
    public class StockModel : IEntity<long>, ICreateAudit, IUpdateAudit
    {
        public long Id { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public long ProductId { get; private set; }
        public string ProductCode { get; private set; }
        public int AvailableStock { get; private set; }
        public long StockActionId { get; private set; }

        public StockModel(long productId, string productCode, int availableStock, long stockActionId) : this(default, DateTime.UtcNow, DateTime.UtcNow, productId, productCode, availableStock, stockActionId)
        {
        }

        public StockModel(long id, DateTime createdOn, DateTime updatedOn, long productId, string productCode, int availableStock, long stockActionId)
        {
            Id = id;
            CreatedOn = createdOn;
            UpdatedOn = updatedOn;
            ProductId = productId;
            ProductCode = productCode;
            AvailableStock = availableStock;
            StockActionId = stockActionId;
        }

        public void UpdateStock(int availableStock, long stockActionId)
        {
            AvailableStock = availableStock;
            StockActionId = stockActionId;
            UpdatedOn = DateTime.UtcNow;
        }
    }
}