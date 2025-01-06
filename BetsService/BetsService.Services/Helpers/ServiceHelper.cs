using BetsService.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BetsService.Services.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddBetsServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddSingleton<EventOutcomesRepository>()
                .AddSingleton<EventsRepository>()
                .AddSingleton<BetsRepository>();

            return services;
        }
    }
}
