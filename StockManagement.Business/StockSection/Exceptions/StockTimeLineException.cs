using System;
using StockManagement.Exceptions;

namespace StockManagement.Business.StockSection.Exceptions
{
    public class StockTimeLineException : ConflictException
    {
        public StockTimeLineException(long currentStockId, long requestStockId) : base(string.Format(ExceptionMessages.STOCK_NEWERTHANREQUEST, currentStockId, requestStockId))
        {
        }
    }
}