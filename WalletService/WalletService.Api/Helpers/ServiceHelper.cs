using Microsoft.EntityFrameworkCore;
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
                .AddScoped<DbContext, MongoDBContext>();

            return services;
        }
    }
}
