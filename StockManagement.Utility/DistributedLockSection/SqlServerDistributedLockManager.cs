using System;
using System.Threading.Tasks;
using Medallion.Threading.Sql;

namespace StockManagement.Utility.DistributedLockSection
{
    public class SqlServerDistributedLockManager : IDistributedLockManager
    {
        private readonly string _connectionString;

        public SqlServerDistributedLockManager(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Lock(string key, Action action)
        {
            var l = new SqlDistributedLock(key, _connectionString);
            using (l.Acquire())
            {
                action();
            }
        }

        public async Task LockAsync(string key, Func<Task> action)
        {
            var l = new SqlDistributedLock(key, _connectionString);
            using (await l.AcquireAsync())
            {
                await action();
            }
        }
    }
}