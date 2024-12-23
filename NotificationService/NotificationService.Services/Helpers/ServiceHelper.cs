using Microsoft.Extensions.DependencyInjection;
using Bets.Abstractions.DataAccess.EF.Repositories;
using NotificationService.Domain;
using NotificationService.Domain.Directories;
using NotificationService.DataAccess.Repositories;
using NotificationService.MailServices;
using NotificationService.Services.HostedServices;

namespace NotificationService.Services.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceCollection AddNotificationServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(MappingProfile))

                .AddScoped<CreatedEntityRepository<IncomingMessages>>()
                .AddScoped<LaterDeletedEntityRepository<Messengers>>()
                .AddScoped<LaterDeletedEntityRepository<Bettors>>()
                .AddScoped<LaterDeletedEntityRepository<MessageSources>>()
                .AddSingleton(typeof(IBettorAddressRepository), typeof(BettorAddressRepository))
                .AddSingleton<IncomingMessagesRepository>(); 

            return services;
        }

        public static IServiceCollection AddNotificationSendingServices(this IServiceCollection services)
        {
            services
                .AddTransient<IMailService, MailService>()
                .AddHostedService<SendingHostedService>();

            return services;
        }
    }
}
