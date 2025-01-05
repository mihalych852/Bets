using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WalletService.RabbitConsumers.Consumers;
using WalletService.RabbitConsumers.Settings;
using WalletService.RabbitConsumers.Helpers;
using WalletService.Service.Helpers;

namespace WalletService.RabbitConsumers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddWalletServices()
                        .AddServices(configuration)
                        .AddMassTransit(x =>
                        {
                            x.AddConsumer<WalletAddConsumer>();
                            x.UsingRabbitMq((context, cfg) =>
                            {
                                ConfigureRmq(cfg, configuration);
                                RabbitHelper.RegisterEndPoints(cfg, context);
                            });
                        });
                    services.AddHostedService<MasstransitService>();
                });
        }

        /// <summary>
        /// Конфигурирование RMQ.
        /// </summary>
        /// <param name="configurator"> Конфигуратор RMQ. </param>
        /// <param name="configuration"> Конфигурация приложения. </param>
        private static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
        {
            ushort rabitPort;
            var strPort = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PORT");
            if (!ushort.TryParse(strPort, out rabitPort))
                throw new ArgumentException("Не удалось преобразовать порт в число");

            string? rabbitHost = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_HOST");

            if (string.IsNullOrEmpty(rabbitHost))
                rabbitHost = "rabbitMq";

            var rmqSettings = new RmqSettings()
            {
                Host = rabbitHost,
                VHost = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_VHOST") ?? "/",
                Login = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_USER") ?? "",
                Password = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PASSWORD") ?? "",
                Port = rabitPort,
            };

            configurator.Host(rmqSettings.Host,
                rmqSettings.Port,
                rmqSettings.VHost,
                h =>
                {
                    h.Username(rmqSettings.Login);
                    h.Password(rmqSettings.Password);
                });
        }
    }
}
