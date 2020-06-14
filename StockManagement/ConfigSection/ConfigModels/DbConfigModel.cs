using System;
using System.Linq;

namespace StockManagement.ConfigSection.ConfigModels
{
    public class DbConfigModel
    {
        public int SelectedIndex { get; set; }
        public DbOption[] DbOptions { get; set; }

        public DbOption SelectedDbOption()
        {
            if (DbOptions == null)
                throw new ArgumentNullException(nameof(DbOptions));

            if (!DbOptions.Any())
                throw new ArgumentException($"{nameof(DbOptions)} is empty");

            DbOption dbOption = DbOptions.FirstOrDefault(o => o.Index == SelectedIndex);

            if (dbOption == null)
                throw new ArgumentOutOfRangeException($"DbOption could not found. {nameof(SelectedIndex)} : {SelectedIndex}");

            return dbOption;
        }
    }

    public class DbOption
    {
        public int Index { get; set; }
        public DbTypes DbType { get; set; }
        public string ConnectionStr { get; set; }
    }

    public enum DbTypes
    {
        SqlServer = 1
    }
}