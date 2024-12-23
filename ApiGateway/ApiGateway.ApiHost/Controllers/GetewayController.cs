using ApiGateway.ApiHost.Service;
using ApiGateway_Core.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ApiGateway.ApiHost.Controllers
{
    [ApiController]
    [Route("bets")]
    public class GetewayController : Controller
    {
        //public readonly MicroservicesConfig _service;
        private readonly HttpRequestHandler _httpRequestHandler;
        public GetewayController(HttpRequestHandler httpRequestHandler) 
        {
            _httpRequestHandler = httpRequestHandler;
        }

        [HttpGet("{serviceName}/{actionName}")]
        public async Task<IActionResult> GetDataService(string serviceName, string actionName,[FromQuery]string? path)
        {
            var result = await _httpRequestHandler.SendRequestAsyncGet(serviceName, actionName, HttpContext, path);

            return result;
        }

        [HttpPost("{serviceName}/{actionName}")]
        public async Task<IActionResult> PostDataService(string serviceName, string actionName,
            [FromQuery] string? path, [FromBody] object? body)
        {
            var result = await _httpRequestHandler.SendRequestAsyncPost(serviceName, actionName,HttpContext,path, body);

            if (result == null)
                return NotFound($"Endpoint for action '{actionName}' not found.");

            return result;
        }

        [HttpPut("{serviceName}/{actionName}")]
        public async Task<IActionResult> PutDataService(string serviceName, string actionName,
            [FromQuery] string? path, [FromBody] object? body)
        {
            var result = await _httpRequestHandler.SendRequestAsyncPut(serviceName, actionName, HttpContext, path, body);

            if (result == null)
                return NotFound($"Endpoint for action '{actionName}' not found.");

            return result;
        }

        [HttpDelete("{serviceName}/{actionName}")]
        public async Task<IActionResult> DeleteDataService(string serviceName, string actionName, [FromQuery] string? path)
        {
            var result = await _httpRequestHandler.SendRequestAsyncDelete(serviceName, actionName, HttpContext, path);

            if (result == null)
                return NotFound($"Endpoint for action '{actionName}' not found.");

            return result;
        }
    } 
}
