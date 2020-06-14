using System;
using System.Linq;

namespace StockManagement.ConfigSection.ConfigModels
{
    public class DistributedLockConfigModel
    {
        public int SelectedIndex { get; set; }
        public DistributedLockOption[] DistributedLockOptions { get; set; }

        public DistributedLockOption SelectedDistributedLockOption()
        {
            if (DistributedLockOptions == null)
                throw new ArgumentNullException(nameof(DistributedLockOptions));

            if (!DistributedLockOptions.Any())
                throw new ArgumentException($"{nameof(DistributedLockOptions)} is empty");

            DistributedLockOption distributedLockOption = DistributedLockOptions.FirstOrDefault(o => o.Index == SelectedIndex);

            if (distributedLockOption == null)
                throw new ArgumentOutOfRangeException($"DistributedLockOption could not found. {nameof(SelectedIndex)} : {SelectedIndex}");

            return distributedLockOption;
        }
    }

    public class DistributedLockOption
    {
        public int Index { get; set; }
        public DistributedLockTypes DistributedLockType { get; set; }
        public string ConnectionStr { get; set; }
    }

    public enum DistributedLockTypes
    {
        SqlServer = 1
    }
}