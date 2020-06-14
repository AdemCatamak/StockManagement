namespace StockManagement.Data.Models.BaseModels
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}