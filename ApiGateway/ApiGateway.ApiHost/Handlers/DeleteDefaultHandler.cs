using ApiGateway.ApiHost.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace ApiGateway.ApiHost.Handlers
{
    public class DeleteDefaultHandler : IRequestHandler<DefaultRequestDelete, IActionResult>
    {
        public async Task<IActionResult> Handle(DefaultRequestDelete request, CancellationToken cancellationToken)
        {
            var response = await request.HttpClient.DeleteAsync(request.requstUrl);

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
