using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace StockManagement.Utility.IntegrationEventHandlerSection
{
    public class IntegrationEventHandler : IIntegrationEventHandler,
                                           IDisposable
    {
        private readonly IBusControl _busControl;
        private readonly ConcurrentBag<IIntegrationEvent> _events;

        public IntegrationEventHandler(IBusControl busControl)
        {
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
            _events = new ConcurrentBag<IIntegrationEvent>();
        }

        public IReadOnlyList<IIntegrationEvent> IntegrationEvents => _events.ToList().AsReadOnly();

        public void AddEvent(IIntegrationEvent integrationEvent)
        {
            _events.Add(integrationEvent);
        }

        public async Task Publish(CancellationToken cancellationToken = default)
        {
            Task[] publishTasks = _events.Select(e => _busControl.Publish(e, e.GetType(), cancellationToken))
                                         .ToArray();
            await Task.WhenAll(publishTasks);
        }

        public void Dispose()
        {
            _events.Clear();
        }
    }
}