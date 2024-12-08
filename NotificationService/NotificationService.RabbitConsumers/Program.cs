using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.RabbitConsumers.Consumers;
using NotificationService.RabbitConsumers.Helpers;
using NotificationService.RabbitConsumers.Settings;
using NotificationService.Services.Helpers;


namespace NotificationService.RabbitConsumers;
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
                    .AddNotificationServices()
                    .AddServices(configuration)
                    .AddMassTransit(x =>
                    {
                        x.AddConsumer<IncomingMessageConsumer>(); 
                        x.UsingRabbitMq((context, cfg) =>
                        {
                            ConfigureRmq(cfg, configuration);
                            RegisterEndPoints(cfg, context);
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
        var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings;

        //var rmqSettings = new RmqSettings()
        //{
        //    Host = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_HOST") ?? "localhost",
        //    VHost = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_VHOST") ?? "/",
        //    Login = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_USER") ?? "",
        //    Password = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PASSWORD") ?? ""
        //};

        configurator.Host(rmqSettings.Host, 
            rmqSettings.Port,
            rmqSettings.VHost,
            h =>
            {
                h.Username(rmqSettings.Login);
                h.Password(rmqSettings.Password);
            });
    }

    /// <summary>
    /// регистрация эндпоинтов
    /// </summary>
    /// <param name="configurator"></param>
    private static void RegisterEndPoints(IRabbitMqBusFactoryConfigurator configurator, IRegistrationContext context)
    {
        configurator.ReceiveEndpoint($"masstransit_incomingmessage_queue", e =>
        {
            e.Consumer<IncomingMessageConsumer>(context);
            e.UseMessageRetry(r =>
            {
                r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            });
            e.PrefetchCount = 1;
            e.UseConcurrencyLimit(1);
        });

    }
}