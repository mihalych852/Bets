using System.Security.Claims;

namespace ApiGateway.ApiHost.Requests
{
    public class DefaultRequestGet : DefaultRequestAbstract
    {
        /// <summary>
        /// Параметры гет запроса
        /// </summary>
        public required string? Parameters { get; set; }
        public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
        {

            return new DefaultRequestGet { Parameters = parameters, requstUrl = GetServiceUrl(requstUrl, parameters),
                HttpClient = httpClient};
        }
    }
}
