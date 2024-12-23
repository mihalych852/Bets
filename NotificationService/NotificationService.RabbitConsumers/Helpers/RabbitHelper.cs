using MassTransit;
using NotificationService.RabbitConsumers.Consumers;

namespace NotificationService.RabbitConsumers.Helpers
{
    public static class RabbitHelper
    {

        /// <summary>
        /// Регистрация эндпоинтов
        /// </summary>
        /// <param name="configurator"></param>
        public static void RegisterEndPoints(IRabbitMqBusFactoryConfigurator configurator, IRegistrationContext context)
        {
            configurator.RegisterEndpoint<IncomingMessageAddConsumer>(context, "masstransit_incomingmessage_queue")
                .RegisterEndpoint<DefaultUserInfoAddConsumer>(context, "masstransit_userinfo_queue");
        }

        private static IRabbitMqBusFactoryConfigurator RegisterEndpoint<T>(this IRabbitMqBusFactoryConfigurator configurator
            , IRegistrationContext context
            , string queueName) where T : class, IConsumer
        {
            configurator.ReceiveEndpoint(queueName, e =>
            {
                e.Consumer<T>(context);
                e.UseMessageRetry(r =>
                {
                    r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                });
                e.PrefetchCount = 1;
                e.UseConcurrencyLimit(1);
            });
            return configurator;
        }
    }
}
