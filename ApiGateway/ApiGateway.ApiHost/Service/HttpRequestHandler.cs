using ApiGateway.ApiHost.Exceptions;
using ApiGateway.ApiHost.Requests;
using ApiGateway_Core.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ApiGateway.ApiHost.Service
{
    public class HttpRequestHandler
    {
        private readonly HttpClient _httpClient;
        private readonly MicroservicesConfig _servicesConfig;
        private readonly IMediator _mediator;

        public HttpRequestHandler(HttpClient httpClient, IOptions<MicroservicesConfig> servicesConfig, IMediator mediator)
        {
            _httpClient = httpClient;
            _servicesConfig = servicesConfig.Value;
            _mediator = mediator;
        }

        public async Task<IActionResult> SendRequestAsyncDelete(string serviceName, string action, HttpContext httpContext, string? requstParametr)
        {
            try
            {
                var requst = new RequestCreatedFactory(_servicesConfig)
                .GetEndpountConfig(serviceName, action)
                .AddParametrRequest(requstParametr)
                .GetServiceUrl(requstParametr)
                .AddHttpParametrs(_httpClient, httpContext)
                .IsAuthorization()
                .GetRequest(HttpMethod.Delete);

                var result = await _mediator.Send(requst);

                return result;
            }
            catch (ArgumentNullException ex)
            {
                return GetResultException(ex.Message, 500);
            }
            catch (TokenValidateException ex)
            {
                return GetResultException(ex.Message, 401);
            }
            catch (Exception ex)
            {

                return GetResultException(ex.Message, 500);
            }
        }

        public async Task<IActionResult> SendRequestAsyncPut(string serviceName, string action,
            HttpContext httpContext, string? parametrs, object? payload = null)
        {
            try
            {
                var requst = new RequestCreatedFactory(_servicesConfig)
                .GetEndpountConfig(serviceName, action)
                .AddParametrRequest(parametrs, payload)
                .GetServiceUrl(parametrs)
                .AddHttpParametrs(_httpClient, httpContext)
                .IsAuthorization()
                .GetRequest(HttpMethod.Put);

                var result = await _mediator.Send(requst);

                return result;
            }
            catch(ArgumentNullException ex)
            {
                return GetResultException(ex.Message, 500);
            }
            catch(TokenValidateException ex)
            {
                return GetResultException(ex.Message, 401);
            }
            catch (Exception ex)
            {

                return GetResultException(ex.Message, 500);
            }
        }

        public async Task<IActionResult> SendRequestAsyncGet(string serviceName, string action, HttpContext httpContext, string? requstParametr)
        {
            try
            {
                var request = new RequestCreatedFactory(_servicesConfig)
                    .GetEndpountConfig(serviceName, action)
                    .AddParametrRequest(requstParametr)
                    .GetServiceUrl(requstParametr)
                    .AddHttpParametrs(_httpClient, httpContext)
                    .IsAuthorization()
                    .GetRequest(HttpMethod.Get);

                var result = await _mediator.Send(request);

                return result;
            }
            catch (ArgumentNullException ex)
            {
                return GetResultException(ex.Message, 500);
            }
            catch (TokenValidateException ex)
            {
                return GetResultException(ex.Message, 401);
            }
            catch (Exception ex)
            {

                return GetResultException(ex.Message, 500);
            }
        }

        public async Task<IActionResult> SendRequestAsyncPost(string serviceName, string action, 
            HttpContext httpContext, string? parametrs, object? payload = null)
        {
            try
            {
                var requst = new RequestCreatedFactory(_servicesConfig)
                .GetEndpountConfig(serviceName, action)
                .AddParametrRequest(parametrs, payload)
                .GetServiceUrl(parametrs)
                .AddHttpParametrs(_httpClient, httpContext)
                .IsAuthorization()
                .GetRequest(HttpMethod.Post);

                var result = await _mediator.Send(requst);

                return result;
            }
            catch (ArgumentNullException ex)
            {
                return GetResultException(ex.Message, 500);
            }
            catch (TokenValidateException ex)
            {
                return GetResultException(ex.Message, 401);
            }
            catch (Exception ex)
            {

                return GetResultException(ex.Message, 500);
            }

        }

        private IActionResult GetResultException(string message, int statusCode)
        {
            return new ContentResult
            {
                Content = message,
                StatusCode = statusCode
            };
        }
    }
}
