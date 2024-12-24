using BetsService.Models;
using MassTransit;
using Sagas.Contracts;
using Sagas.States;
using WalletService.Models;

namespace Sagas.BetsSagas
{
    public class MakeBetsSaga : MassTransitStateMachine<MakeBetsSagaState>
    {
        private readonly ILogger<MakeBetsSaga> _logger;

        public MakeBetsSaga(ILogger<MakeBetsSaga> logger)
        {
            _logger = logger;

            //Указываем куда будем записывать текущее состояние саги (Pending,Faulted)
            InstanceState(x => x.CurrentState);
            //Указываем что слушаем событие BetsId у которого равен нашему CorrelationId у саги
            //Либо если нет саги с таким CorrelationId то создаем его с ним.
            Event<MakeBetsRequest>(() => MakeBets, x => x.CorrelateById(y => y.Message.BetsId));
            //Указываем какие запросы будем делать из саги
            Request( () => GetMoney );
            Request( () => GetBets );

            //Указываем как будем реагировать на сообщения в стартовом состоянии
            Initially(
                When(MakeBets)
                .Then(x =>
                {
                    //Сохраняем идентификатор запроса и его адрес при старте саги чтобы потом на него ответить
                    if (!x.TryGetPayload(out SagaConsumeContext<MakeBetsSagaState, MakeBetsRequest> payload))
                    {
                        throw new Exception("Unable to retrieve required payload for callback data.");
                    }
                    x.Saga.RequestId = payload.RequestId;
                    x.Saga.ResponseAddress = payload.ResponseAddress;
                })
                //Совершаем запрос к микросевису WalletService
                .Request(GetMoney, x => x.Init<TransactionsRequest>(new { x.Message.BetsId }))
                //Переводим сагу в состояние GetMoney.Pending
                .TransitionTo(GetMoney.Pending)
                );

            //Описываем то как наша сага будет реагировать на сообщения находясь в 
            //состоянии GetMoney.Pending
            During(GetMoney.Pending,
                //Когда приходит сообщение что запрос прошел успешно делаем новый запрос
                //теперь уже в микросервис BetsService
                When(GetMoney.Completed)
                .Request(GetBets, x => x.Init<BetsRequest>(new { x.Message.TransactionId }))
                .TransitionTo(GetBets.Pending),
                //При ошибке отвечаем тому, кто инициировал запрос сообщением с текстом ошибки
                When(GetMoney.Faulted)
                  .ThenAsync(async context =>
                  {
                      //Тут можно сделать какие-то компенсирующие действия. 
                      //Например, вернуть деньги куда-то на счет.
                      await RespondFromSaga(context, "Faulted On Get Money " + string.Join("; ", context.Message.Exceptions.Select(x => x.Message)));
                  })
                .TransitionTo(Failed),
                //При таймауте отвечаем с сообщением что произошел таймаут
                When(GetMoney.TimeoutExpired)
                   .ThenAsync(async context =>
                   {
                       await RespondFromSaga(context, "Timeout Expired On Get Money");
                   })
                .TransitionTo(Failed)
                 );


        }

        public Request<MakeBetsSagaState, BetsRequest, IGetMoneyResponse> GetBets { get; set; }
        public Request<MakeBetsSagaState, TransactionsRequest, IGetItemsResponse> GetMoney { get; set; }
        public Event<MakeBetsRequest> MakeBets { get; set; }
        public State Failed { get; set; }

        private static async Task RespondFromSaga<T>(BehaviorContext<MakeBetsSagaState, T> context, string error) where T : class
        {
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
            await endpoint.Send(new MakeBetsResponse
            {
                BetsId = context.Saga.CorrelationId,
                ErrorMessage = error
            }, r => r.RequestId = context.Saga.RequestId);
        }
    }

    public interface IGetMoneyResponse
    {
        int BetsId { get; set; }
    }

    public interface IGetItemsResponse
    {
        int TransactionId { get; set; }
    }
}
