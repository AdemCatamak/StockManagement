using System;

namespace StockManagement.Api.Contracts.StockRequests
{
    public class GetStockCollectionHttpRequest : BaseCollectionHttpRequest
    {
        public long? StockId { get; set; }
        public long? ProductId { get; set; }
        public string ProductCode { get; set; }
        public DateTime? StockUpdatedLaterThan { get; set; }
    }
}