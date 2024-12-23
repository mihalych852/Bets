using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.ApiHost.Handlers
{
    public class GetDefaultHandler : IRequestHandler<DefaultRequestGet, IActionResult>
    {
        public async Task<IActionResult> Handle(DefaultRequestGet request, CancellationToken cancellationToken)
        {
            var response = await request.HttpClient.GetAsync(request.requstUrl);

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
