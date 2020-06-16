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
        private readonly ConcurrentQueue<IIntegrationEvent> _events;

        public IntegrationEventHandler(IBusControl busControl)
        {
            _busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
            _events = new ConcurrentQueue<IIntegrationEvent>();
        }

        public IReadOnlyList<IIntegrationEvent> IntegrationEvents => _events.ToList().AsReadOnly();

        public void AddEvent(IIntegrationEvent integrationEvent)
        {
            _events.Enqueue(integrationEvent);
        }

        public async Task Publish(CancellationToken cancellationToken = default)
        {
            var publishTasks = new List<Task>();

            while (_events.TryDequeue(out IIntegrationEvent integrationEvent))
            {
                Task publishTask = _busControl.Publish(integrationEvent, integrationEvent.GetType(), cancellationToken);
                publishTasks.Add(publishTask);
            }

            await Task.WhenAll(publishTasks);
        }

        public void Dispose()
        {
            _events.Clear();
        }
    }
}