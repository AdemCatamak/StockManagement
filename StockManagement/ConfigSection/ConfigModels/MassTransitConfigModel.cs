using System;
using System.Collections.Generic;
using System.Linq;

namespace StockManagement.ConfigSection.ConfigModels
{
    public class MassTransitConfigModel
    {
        public List<MassTransitOption> MassTransitOptions { get; set; }
        public int SelectedIndex { get; set; }
        public int BusStartStartTimeoutSeconds { get; set; }
        public int BusStartStopTimeoutSeconds { get; set; }
        public int RetryLimitCount { get; set; }
        public int InitialIntervalSeconds { get; set; }
        public int IntervalIncrementSeconds { get; set; }
        public int ConcurrencyLimit { get; set; }

        public MassTransitOption SelectedMassTransitOption()
        {
            if (MassTransitOptions == null)
                throw new ArgumentNullException(nameof(MassTransitOptions));

            if (!MassTransitOptions.Any())
                throw new ArgumentException($"{nameof(MassTransitOptions)} is empty");

            MassTransitOption massTransitOption = MassTransitOptions.FirstOrDefault(o => o.Index == SelectedIndex);

            if (massTransitOption == null)
                throw new ArgumentOutOfRangeException($"MassTransitOption could not found. {nameof(SelectedIndex)} : {SelectedIndex}");

            return massTransitOption;
        }
    }

    public class MassTransitOption
    {
        public int Index { get; set; }
        public MassTransitBrokerTypes BrokerType { get; set; }
        public string BrokerName { get; set; }
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public enum MassTransitBrokerTypes
    {
        RabbitMq = 1,
    }
}