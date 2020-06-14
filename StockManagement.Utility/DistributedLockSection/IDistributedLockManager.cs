using System;
using System.Threading.Tasks;

namespace StockManagement.Utility.DistributedLockSection
{
    public interface IDistributedLockManager
    {
        void Lock(string key, Action action);
        Task LockAsync(string key, Func<Task> action);
    }
}