namespace ApiGateway.ApiHost.Requests
{
    public class UserServerRequest : DefaultRequestAbstract
    {
       public string? Token { get; set; }
       public string? Method { get; set; }
       public object? Payload { get; set; }

       public override DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext)
       {
           var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

           return new UserServerRequest { Token = token, requstUrl = requstUrl, Payload = payload, HttpClient = httpClient, Method = httpContext.Request.Method };
       }
    }
}
