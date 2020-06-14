using System;
using StockManagement.Data.Models.BaseModels;

namespace StockManagement.Data.Models
{
    public class ProductModel : IEntity<long>, ICreateAudit
    {
        public long Id { get; private set; }
        public string ProductCode { get; private set; }
        public DateTime CreatedOn { get; private set; }


        public ProductModel(string productCode) : this(default, productCode, DateTime.UtcNow)
        {
        }

        public ProductModel(long id, string productCode, DateTime createdOn)
        {
            Id = id;
            ProductCode = productCode;
            CreatedOn = createdOn;
        }
    }
}