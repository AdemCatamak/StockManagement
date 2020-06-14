using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using StockManagement.ConfigSection.ConfigModels;

namespace StockManagement.HostedServices
{
    public class BusControlStarterHostedService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly MassTransitConfigModel _massTransitConfigModel;

        public BusControlStarterHostedService(IBusControl busControl, MassTransitConfigModel massTransitConfigModel)
        {
            _busControl = busControl;
            _massTransitConfigModel = massTransitConfigModel;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var startCts = new CancellationTokenSource(TimeSpan.FromSeconds(_massTransitConfigModel.BusStartStartTimeoutSeconds)))
            {
                CancellationToken startCancellationToken = startCts.Token;

                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, startCancellationToken))
                {
                    await _busControl.StartAsync(linkedCts.Token);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using (var stopCts = new CancellationTokenSource(TimeSpan.FromSeconds(_massTransitConfigModel.BusStartStopTimeoutSeconds)))
            {
                CancellationToken stopCancellationToken = stopCts.Token;

                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, stopCancellationToken))
                {
                    await _busControl.StopAsync(linkedCts.Token);
                }
            }
        }
    }
}