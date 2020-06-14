using System;

namespace StockManagement.Data.Models.BaseModels
{
    public interface ICreateAudit
    {
        DateTime CreatedOn { get; }
    }
}