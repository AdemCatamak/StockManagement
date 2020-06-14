namespace StockManagement.Exceptions
{
    public abstract class ValidationException : BaseException
    {
        protected ValidationException(string message) : base(message)
        {
        }
    }
}