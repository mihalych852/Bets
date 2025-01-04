
namespace ApiGateway.ApiHost.Requests
{
    public class IdByUrlRequest : DefaultRequestAbstract
    {
        public string? Method { get; set; }
        public object? Payload { get; set; }
        public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
        {
            return new IdByUrlRequest
            {
                requstUrl = GetServiceUrl(requstUrl, parameters),
                Payload = payload,
                HttpClient = httpClient,
                Method = httpContext.Request.Method
            };
        }

        public override string GetServiceUrl(string requstUrl, string? parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                return requstUrl += "/" + parameters;
            }

            return requstUrl;
        }
    }
}
