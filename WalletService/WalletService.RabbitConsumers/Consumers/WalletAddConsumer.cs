using MassTransit;
using Microsoft.Extensions.Logging;
using WalletService.Models;
using WalletService.Service;

namespace WalletService.RabbitConsumers.Consumers
{
    public class WalletAddConsumer : IConsumer<AddWalletRequest>
    {
        private readonly ILogger<WalletAddConsumer> _logger;
        private readonly WalletsService _walletsService;

        public WalletAddConsumer(ILogger<WalletAddConsumer> logger
            , WalletsService walletsService)
        {
            _logger = logger;
            _walletsService = walletsService;
        }

        public async Task Consume(ConsumeContext<AddWalletRequest> context)
        {
            try
            {
                await _walletsService.AddWalletAsync(context.Message.BettorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
