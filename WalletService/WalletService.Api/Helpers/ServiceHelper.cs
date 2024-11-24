using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
    }
}
