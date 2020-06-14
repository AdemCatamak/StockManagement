using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace StockManagement.MassTransitObservers
{
    public class BasicPublishObserver : IPublishObserver
    {
        private readonly ILogger<BasicPublishObserver> _logger;

        public BasicPublishObserver(ILogger<BasicPublishObserver> logger)
        {
            _logger = logger;
        }

        public Task PrePublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogInformation(
                                   $"{context.DestinationAddress} - Message is publishing - Message Id :{context.MessageId}{Environment.NewLine}" +
                                   $"{JsonConvert.SerializeObject(context.Message)}");
            return Task.CompletedTask;
        }

        public Task PostPublish<T>(PublishContext<T> context) where T : class
        {
            _logger.LogInformation(
                                   $"{context.DestinationAddress} - Message is published - Message Id :{context.MessageId}{Environment.NewLine}" +
                                   $"{JsonConvert.SerializeObject(context.Message)}");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception) where T : class
        {
            _logger.LogError(
                             exception,
                             $"{context.DestinationAddress} - Message could not published - Message Id :{context.MessageId}{Environment.NewLine}" +
                             $"{JsonConvert.SerializeObject(context.Message)}");
            return Task.CompletedTask;
        }
    }
}