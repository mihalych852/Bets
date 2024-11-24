namespace ApiGateway.ApiHost.Requests
{
    public class LogutRequest : DefaultRequestAbstract
    {
       public string? Token { get; set; }
        public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            return new LogutRequest { Token = token, requstUrl = requstUrl, HttpClient = httpClient };
        }
    }
}
