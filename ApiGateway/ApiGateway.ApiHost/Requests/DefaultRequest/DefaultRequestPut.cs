
namespace ApiGateway.ApiHost.Requests
{
    public class DefaultRequestPut : DefaultRequestAbstract
    {
        public object? Payload { get; set; }

        public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
        {
            return new DefaultRequestPut { Payload = payload, requstUrl = GetServiceUrl(requstUrl, parameters),
                HttpClient = httpClient };
        }
    }
}
