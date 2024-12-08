using MassTransit;
using NotificationService.RabbitConsumers.Consumers;

namespace NotificationService.RabbitConsumers.ConsumerDefinitions
{
    public class IncomingMessageConsumerDefinition : ConsumerDefinition<IncomingMessageConsumer>, IConsumerDefinition<IncomingMessageConsumer>
    {
        public IncomingMessageConsumerDefinition()
        {
            // override the default endpoint name
            EndpointName = "masstransit_incomingmessage_queue_1";

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            //ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<IncomingMessageConsumer> consumerConfigurator, IRegistrationContext context)
        {
            endpointConfigurator.PrefetchCount = 1;
            endpointConfigurator.UseConcurrencyLimit(1);
            endpointConfigurator.UseMessageRetry(r => r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1)));

            // use the outbox to prevent duplicate events from being published
            //endpointConfigurator.UseInMemoryOutbox(context);
        }
    }
}
