using MassTransit;
using Sagas.States;

namespace Sagas.BetsSagas
{
    public class MakeBetsSaga
    {

        public Request<MakeBetsSagaState, IGetMoneyRequest, IGetMoneyResponse> GetBets { get; set; }
        public Request<MakeBetsSagaState, IGetItemsRequest, IGetItemsResponse> GetWallet { get; set; }
        public Event<BuyItemsRequest> BuyItems { get; set; }
        public State Failed { get; set; }

        private static async Task RespondFromSaga<T>(BehaviorContext<MakeBetsSagaState, T> context, string error) where T : class
        {
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
            await endpoint.Send(new BuyItemsResponse
            {
                OrderId = context.Saga.CorrelationId,
                ErrorMessage = error
            }, r => r.RequestId = context.Saga.RequestId);
        }
    }
}
