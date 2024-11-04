using Bets.Abstractions.DataAccess.EF.Repositories;
using Microsoft.Extensions.DependencyInjection;
using WalletService.DataAccess.Repositories;
using WalletService.Domain;

namespace WalletService.Service.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddWalletServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddScoped<CreatedEntityRepository<Transactions>>()
                .AddScoped<WalletsRepository>();

            return services;
        }
    }
}
