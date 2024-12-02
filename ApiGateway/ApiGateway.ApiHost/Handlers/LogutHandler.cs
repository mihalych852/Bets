using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiGateway.ApiHost.Handlers
{
    public class LogutHandler : IRequestHandler<LogutRequest, IActionResult>
    {
        public async Task<IActionResult> Handle(LogutRequest request, CancellationToken cancellationToken)
        {
            request.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token);
            var response = await request.HttpClient.PostAsync(request.requstUrl, null);

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
