using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Models;
using NotificationService.Services;

namespace NotificationService.RabbitConsumers.Consumers
{
    public class DefaultUserInfoAddConsumer : IConsumer<DefaultUserInfo>
    {
        private readonly ILogger<DefaultUserInfoAddConsumer> _logger;
        private readonly BettorsService _bettorsService;
        private readonly BettorAddressesService _addressesService;

        public DefaultUserInfoAddConsumer(ILogger<DefaultUserInfoAddConsumer> logger
            , BettorsService bettorsService
            , BettorAddressesService addressesService)
        {
            _logger = logger;
            _bettorsService = bettorsService;
            _addressesService = addressesService;
        }

        public async Task Consume(ConsumeContext<DefaultUserInfo> context)
        {
            try
            {
                _ = await _bettorsService.AddBettorAsync(context.Message.Bettor, CancellationToken.None);
                _ = _addressesService.AddBettorAddressesAsync(context.Message.Address, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
