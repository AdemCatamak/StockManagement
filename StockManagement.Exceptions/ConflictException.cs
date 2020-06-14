namespace StockManagement.Exceptions
{
    public abstract class ConflictException : BaseException
    {
        protected ConflictException(string message) : base(message)
        {
        }
    }
}