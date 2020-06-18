namespace StockManagement.Business
{
    public class ExceptionMessages
    {
        public const string OFFSET_INVALID = "Offset value is invalid";
        public const string TAKE_INVALID = "Take value is invalid";

        public const string PRODUCT_CODE_EMPTY = "Product code should not be empty";
        public const string PRODUCT__NOTFOUND = "Product could not found";
        public const string PRODUCT__ALREADYEXIST = "{0} already exist";

        public const string CORRELATIONID_EMPTY = "Correlation id should not be empty";


        public const string REMOVEFROMSTOCK_COUNT_NOTPOSITIVE = "At least 1 item should be removed from stock";
        public const string REMOVEFROMSTOCK__ALREADYEXECUTED = "Remove from stock operation is already executed with {0} correlation";
        public const string ADDTOSTOCK_COUNT_NOTPOSITIVE = "At least 1 item should be added to stock";
        public const string ADDTOSTOCK__ALREADYEXECUTED = "Add to stock operation is already executed with {0} correlation";

        public const string STOCKSNAPSHOT__NOTFOUND = "Stock snapshot could not found";
        public const string STOCKSNAPSHOT__NOTFOUND_X = "Stock snapshot could not found for {0}";
        public const string STOCK_INSUFFICENT = "{2} of {1} {0} products were requested";
        
        public const string STOCK__NOTFOUND = "Stock could not found";
        public const string STOCK_NEWERTHANREQUEST = "Current stock version [{0}] is newer than requested[{1}]";
    }
}