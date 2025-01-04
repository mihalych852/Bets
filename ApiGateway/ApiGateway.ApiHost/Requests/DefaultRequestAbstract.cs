using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiGateway.ApiHost.Requests
{
    public abstract class DefaultRequestAbstract : IRequest<IActionResult>
    {
        public string? requstUrl { get; set; }
        public HttpClient HttpClient { get; set; } = new HttpClient();
        /// <summary>
        /// Получение параметров запроса
        /// </summary>
        /// <param name="parameters">Параметры запроса</param>
        /// <param name="payload">обект post запроса</param>
        /// <returns>экземпляр параметров</returns>
        public abstract DefaultRequestAbstract GetRequest(HttpClient httpClient, string? parameters, object? payload, string requstUrl, HttpContext httpContext);

        public virtual string GetServiceUrl(string requstUrl, string? parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                return requstUrl += "?" + parameters;
            }

            return requstUrl;
        }
    }
}
