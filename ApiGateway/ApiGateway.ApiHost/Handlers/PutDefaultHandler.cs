using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace ApiGateway.ApiHost.Handlers
{
    public class PutDefaultHandler : IRequestHandler<DefaultRequestPut, IActionResult>
    {
        public async Task<IActionResult> Handle(DefaultRequestPut request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            var content = new StringContent(JsonSerializer.Serialize(request.Payload), Encoding.UTF8, "application/json");
            response = await request.HttpClient.PutAsync(request.requstUrl, content);

            var responseContent = await response.Content.ReadAsStringAsync();

            return new ContentResult
            {
                Content = responseContent,
                ContentType = response.Content.Headers.ContentType?.ToString(),
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
