using Microsoft.EntityFrameworkCore;
using BetsService.Services;
using BetsService.DataAccess;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

namespace BetsService.Api.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            const string connectionName = "betsDb";

            var connectionString = config.GetConnectionString(connectionName);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"Connection string {connectionName} not defined");

            //services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));
            services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(string.Format(connectionString
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"))));

            services
                .AddScoped<EventsService>()
                .AddScoped<EventOutcomesService>()
                .AddScoped<BettingService>()
                .AddScoped<DbContext, DatabaseContext>();

            return services;
        }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration config)
        {
            var conntectionResdis = config.GetConnectionString("Redis");
            if (String.IsNullOrEmpty(conntectionResdis))
                throw new NullReferenceException(nameof(conntectionResdis));

            conntectionResdis = string.Format(conntectionResdis,
                Environment.GetEnvironmentVariable("ASPNETCORE_REDISPORT"),
                Environment.GetEnvironmentVariable("ASPNETCORE_REDIS_PASSWORD"));

            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = conntectionResdis;
                option.InstanceName = "BetsService";
            });

            return services;
        }
        
        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app_name", "BetsServer")
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
