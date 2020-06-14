using Microsoft.Extensions.Configuration;
using StockManagement.ConfigSection.ConfigModels;

namespace StockManagement.ConfigSection
{
    public static class AppConfigs
    {
        public class ConfigKeys
        {
            public const string DbConfig = "DbConfig";
            public const string AppUrls = "AspNetCoreUrls";
            public const string DistributedLockConfig = "DistributedLockConfig";
            public const string MassTransitConfig = "MassTransitConfig";
        }

        private static IConfiguration _configuration;
        public static IConfiguration Configuration => _configuration ??= GetConfig();

        private static IConfiguration GetConfig()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            PrepareConfig(configurationBuilder);
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            return configurationRoot;
        }

        public static void PrepareConfig(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.AddJsonFile("appsettings.json");
        }

        public static DistributedLockConfigModel GetDistributedLockConfigModel()
        {
            var distributedLockConfigModel = Configuration.GetSection(ConfigKeys.DistributedLockConfig)
                                                          .Get<DistributedLockConfigModel>();

            return distributedLockConfigModel;
        }

        public static DistributedLockOption SelectedDistributedLockOption()
        {
            DistributedLockConfigModel distributedLockConfigModel = GetDistributedLockConfigModel();

            DistributedLockOption distributedLockOption = distributedLockConfigModel.SelectedDistributedLockOption();

            return distributedLockOption;
        }

        public static DbConfigModel GetDbConfigModel()
        {
            var dbConfigModel = Configuration.GetSection(ConfigKeys.DbConfig)
                                             .Get<DbConfigModel>();

            return dbConfigModel;
        }

        public static DbOption SelectedDbOption()
        {
            DbConfigModel dbConfigModel = GetDbConfigModel();

            DbOption dbOption = dbConfigModel.SelectedDbOption();

            return dbOption;
        }

        public static MassTransitConfigModel GetMassTransitConfigModel()
        {
            var massTransitConfigModel = Configuration.GetSection(ConfigKeys.MassTransitConfig)
                                                      .Get<MassTransitConfigModel>();

            return massTransitConfigModel;
        }

        public static MassTransitOption SelectedMassTransitOption()
        {
            MassTransitConfigModel massTransitConfigModel = GetMassTransitConfigModel();

            MassTransitOption massTransitOption = massTransitConfigModel.SelectedMassTransitOption();

            return massTransitOption;
        }

        public static string[] AppUrls()
        {
            string[] urls = Configuration.GetSection(ConfigKeys.AppUrls).Get<string[]>();
            return urls;
        }
    }
}