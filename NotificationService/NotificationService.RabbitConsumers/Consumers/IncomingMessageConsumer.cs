using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.RabbitConsumers.Consumers
{
    public class IncomingMessageConsumer : IConsumer<IncomingMessageRequest>
    {
        private readonly ILogger<IncomingMessageConsumer> _logger;
        private readonly IncomingMessagesService _messagesService;

        //public IncomingMessageConsumer() { }

        public IncomingMessageConsumer(ILogger<IncomingMessageConsumer> logger
            , IncomingMessagesService messagesService)
        {
            _logger = logger;
            _messagesService = messagesService;
        }

        public async Task Consume(ConsumeContext<IncomingMessageRequest> context)
        {
            try
            {
                _ = await _messagesService.AddMessageAsync(context.Message, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
