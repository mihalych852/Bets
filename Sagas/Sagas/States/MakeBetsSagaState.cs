using MassTransit;

namespace Sagas.States
{
    public class MakeBetsSagaState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public Uri? ResponseAddress { get; set; }
    }
}
