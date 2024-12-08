using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.RabbitConsumers.Consumers
{
    public class IncomingMessageAddConsumer : IConsumer<IncomingMessageRequest>
    {
        private readonly ILogger<IncomingMessageAddConsumer> _logger;
        private readonly IncomingMessagesService _messagesService;

        public IncomingMessageAddConsumer(ILogger<IncomingMessageAddConsumer> logger
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
