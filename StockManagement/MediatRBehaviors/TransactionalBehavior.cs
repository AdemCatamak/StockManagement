using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StockManagement.Data;
using StockManagement.Utility.IntegrationEventHandlerSection;

namespace StockManagement.MediatRBehaviors
{
    public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly DataContext _dataContext;
        private readonly IIntegrationEventPublisher _integrationEventPublisher;

        public TransactionalBehavior(DataContext dataContext, IIntegrationEventPublisher integrationEventPublisher)
        {
            _dataContext = dataContext;
            _integrationEventPublisher = integrationEventPublisher;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            IDbContextTransaction dbContextTransaction = null;
            if (_dataContext.Database.CurrentTransaction == null)
            {
                dbContextTransaction = await _dataContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken: cancellationToken);
            }

            TResponse response = await next();

            if (dbContextTransaction != null)
            {
                await dbContextTransaction.CommitAsync(cancellationToken);
                await _integrationEventPublisher.Publish(cancellationToken);
            }

            return response;
        }
    }
}