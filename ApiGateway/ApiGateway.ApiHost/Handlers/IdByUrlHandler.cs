using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace ApiGateway.ApiHost.Handlers
{
    public class IdByUrlHandler : IRequestHandler<IdByUrlRequest, IActionResult>
    {
        public async Task<IActionResult> Handle(IdByUrlRequest request, CancellationToken cancellationToken)
        {
            HttpResponseMessage responseMessage;

            StringContent? content = null;

            if (request.Payload != null)
            {
                content = new StringContent(JsonSerializer.Serialize(request.Payload), Encoding.UTF8, "application/json");
            }

            responseMessage = request.Method switch
            {
                "GET" => await request.HttpClient.GetAsync(request.requstUrl),
                "POST" => await request.HttpClient.PostAsync(request.requstUrl, content),
                "PUT" => await request.HttpClient.PutAsync(request.requstUrl, content),
                "DELETE" => await request.HttpClient.DeleteAsync(request.requstUrl),
                _ => throw new ArgumentException("Htpp метод не поддрерживается")
            };

            var responseContent = await responseMessage.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = responseContent,
                ContentType = responseMessage.Content.Headers.ContentType?.ToString(),
                StatusCode = (int)responseMessage.StatusCode
            };
        }
    }
}
