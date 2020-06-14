using System;

namespace StockManagement.Data.Models.BaseModels
{
    public interface IUpdateAudit
    {
        DateTime UpdatedOn { get; }
    }
}