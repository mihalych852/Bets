using Microsoft.EntityFrameworkCore;
using NotificationService.Services;
using NotificationService.DataAccess;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

namespace NotificationService.Api.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionName = "notificationDb";

            var connectionString = config.GetConnectionString(connectionName);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"Connection string {connectionName} not defined");

            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(string.Format(connectionString
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"))));

            services
                .AddScoped<IncomingMessagesService>()
                .AddScoped<MessengersService>()
                .AddScoped<BettorsService>()
                .AddScoped<MessageSourcesService>()
                .AddScoped<BettorAddressesService>()
                .AddSingleton<SendingService>()
                .AddScoped<DbContext, DatabaseContext>();

            return services;
        }

        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app_name", "NotificationService")
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
    }
}
