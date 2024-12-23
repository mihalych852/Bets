using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ApiGateway.ApiHost.Handlers
{
    public class PostDefaultHandler : IRequestHandler<DefaultRequestPost, IActionResult>
    {
        public async Task<IActionResult> Handle(DefaultRequestPost request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            if (request.Payload != null)
            {
                var content = new StringContent(JsonSerializer.Serialize(request.Payload), Encoding.UTF8, "application/json");
                response = await request.HttpClient.PostAsync(request.requstUrl, content);
            }
            else
            {
                response = await request.HttpClient.GetAsync(request.requstUrl);
            }

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
