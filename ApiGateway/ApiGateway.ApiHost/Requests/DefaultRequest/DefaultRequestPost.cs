using System.Security.Claims;

namespace ApiGateway.ApiHost.Requests
{
    public class DefaultRequestPost : DefaultRequestAbstract
    {
        public object? Payload { get; set; }
        public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
        {
            return new DefaultRequestPost { Payload = payload, requstUrl = GetServiceUrl(requstUrl, parameters),
                HttpClient = httpClient };
        }
    }
}