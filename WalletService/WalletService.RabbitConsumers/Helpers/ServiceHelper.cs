using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WalletService.DataAccess;
using WalletService.Service;

namespace WalletService.RabbitConsumers.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MongoDBSettings>(config.GetSection(nameof(MongoDBSettings)));

            services
                .AddScoped<WalletsService>()
                .AddSingleton<DbContext, MongoDBContext>()

                .AddDbContext<MongoDBContext>();

            return services;
        }
    }
}
