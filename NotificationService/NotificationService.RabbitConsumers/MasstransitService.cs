using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NotificationService.RabbitConsumers
{
    public class MasstransitService : IHostedService
    {
        private IBusControl _busControl;
        private readonly ILogger<MasstransitService> _logger;

        public MasstransitService(ILogger<MasstransitService> logger, IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _busControl.StartAsync(cancellationToken);
            _logger.LogInformation("MasstransitService started.");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken);
            _logger.LogInformation("MasstransitService stopped.");
        }
    }
}
