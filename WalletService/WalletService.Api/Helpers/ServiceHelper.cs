using MassTransit;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WalletService.Api.Settings;
using WalletService.DataAccess;
using WalletService.Service;

namespace WalletService.Api.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDBSettings>(config.GetSection(nameof(MongoDBSettings)));

            services
                .AddScoped<WalletsService>()
                .AddScoped<DbContext, MongoDBContext>()

                .AddDbContext<MongoDBContext>();

            return services;
        }

        public static IServiceCollection AddRabitSetvices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x => {
                x.UsingRabbitMq((context, cfg) =>
                {
                    ConfigureRmq(cfg, configuration);
                });
            });

            return services;
        }

        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app_name", "WalletService")
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response | HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
            });

            return builder;
        }

        public static WebApplication AddHttpLogging<T>(this WebApplication app)
        {
            app.UseHttpLogging();

            // Пример использования логирования
            app.UseSerilogRequestLogging();

            var log = app.Services.GetService<ILogger<T>>();
            log?.LogDebug("debug");
            log?.LogInformation("info");

            return app;
        }

        /// <summary>
        /// Конфигурирование RMQ.
        /// </summary>
        /// <param name="configurator"> Конфигуратор RMQ. </param>
        private static void ConfigureRmq(IRabbitMqBusFactoryConfigurator configurator, IConfiguration configuration)
        {
            //var rmqSettings = configuration.Get<ApplicationSettings>().RmqSettings; 

            ushort rabitPort;
            var strPort = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PORT");
            if (!ushort.TryParse(strPort, out rabitPort))
                throw new ArgumentException("Не удалось преобразовать порт в число");

            var rmqSettings = new RmqSettings()
            {
                Host = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_HOST") ?? "localhost",
                Port = rabitPort,
                VHost = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_VHOST") ?? "/",
                Login = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_USER") ?? "",
                Password = Environment.GetEnvironmentVariable("ASPNETCORE_RABIT_PASSWORD") ?? ""
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
