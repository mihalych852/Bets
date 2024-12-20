using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.DataAccess;
using NotificationService.Models;
using NotificationService.RabbitConsumers.Consumers;
using NotificationService.Services;

namespace NotificationService.RabbitConsumers.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            var connectionName = "notificationDb";

            var connectionString = config.GetConnectionString(connectionName);
            if (string.IsNullOrEmpty(connectionString))
                throw new Exception($"Connection string {connectionName} not defined");

            //connectionString = string.Format(connectionString
            //        , Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE")
            //        , Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER")
            //        , Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD"));

            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContextFactory<DatabaseContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContextFactory<DatabaseContext>(options => options.UseNpgsql(string.Format(connectionString
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBBASE_NOTIFICATION")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBUSER_NOTIFICATION")
                    , Environment.GetEnvironmentVariable("ASPNETCORE_DBPASSWORD_NOTIFICATION"))));

            services
                .AddScoped<IncomingMessagesService>()
                .AddScoped<BettorsService>()
                .AddScoped<BettorAddressesService>()

                .AddScoped<IConsumer<IncomingMessageRequest>, IncomingMessageAddConsumer>()
                .AddScoped<IConsumer<DefaultUserInfo>, DefaultUserInfoAddConsumer>()

                .AddScoped<DbContext, DatabaseContext>();

            return services;
        }
    }
}
