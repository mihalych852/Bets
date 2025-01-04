using ApiGateway.ApiHost.Exceptions;
using ApiGateway.ApiHost.Requests;
using ApiGateway_Core.Configurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text.Json;

namespace ApiGateway.ApiHost.Service
{
    public class RequestCreatedFactory
    {
        private const string TokenValidateServiceName = "user";
        private const string TokenValidateActionName = "ValidateToken";
        private readonly MicroservicesConfig _microservicesConfig;
        private string _serviceName = string.Empty;
        private string _actionName = string.Empty;
        private string _serviceUrl = string.Empty;
        private EndpointConfig _endpointConfig = new EndpointConfig();
        private HttpClient? _httpClient;
        private HttpContext? _httpContext;
        private string? _parametrs;
        private object? _payload;

        /// <summary>
        /// Контруктор фабрики по созданию запроса
        /// </summary>
        /// <param name="servicesConfig">Конфигурация микросервисов</param>
        public RequestCreatedFactory(MicroservicesConfig servicesConfig)
        {
            this._microservicesConfig = servicesConfig;
        }

        /// <summary>
        /// Получить конфигурацию метожа
        /// </summary>
        /// <param name="serviceName">Имя сервиса</param>
        /// <param name="action">Имя метода</param>
        /// <exception cref="ArgumentNullException">Ну удалось найти конфигурацию метода</exception>
        public RequestCreatedFactory GetEndpountConfig(string serviceName, string action)
        {
            this._serviceName = serviceName;
            this._actionName = action;

            if (_microservicesConfig.Services.TryGetValue(_serviceName, out var serviceConfig) &&
               serviceConfig.Endpoints.TryGetValue(_actionName, out var endpointConfig))
            {
                this._endpointConfig = endpointConfig;
            }
            else
            {
                throw new ArgumentNullException(nameof(action), "Сервис не найден");
            }

            return this;
        }

        /// <summary>
        /// Получение ссылки для запроса, будет установлена если метод endpoint htpp
        /// </summary>
        /// <param name="parametrs">Параметры запроса из query</param>
        /// <exception cref="ArgumentNullException">Не найден сервис</exception>
        public RequestCreatedFactory GetServiceUrl(string? parametrs = null)
        {
            if (_endpointConfig.Type != "http")
                return this;


            if (_microservicesConfig.Services.TryGetValue(_serviceName, out var serviceConfig))
            {
                var requestUr = $"{serviceConfig.BaseUrl}/{_endpointConfig.Path}";
                this._serviceUrl =  requestUr;
            }
            else
            {
                throw new ArgumentNullException(nameof(_actionName), "Сервис не найден");
            }

            //if (!string.IsNullOrEmpty(parametrs))
            //{
            //    this._serviceUrl += "?" + parametrs;
            //}

            return this;
        }

        /// <summary>
        /// Добавить клиенты для работы с http
        /// </summary>
        /// <param name="httpClient">Для отправки запроса</param>
        /// <param name="httpContext">Для получения параметров текущего запроса</param>
        /// <returns></returns>
        public RequestCreatedFactory AddHttpParametrs(HttpClient httpClient, HttpContext httpContext)
        {
            this._httpContext = httpContext;
            this._httpClient = httpClient;

            return this;
        }

        /// <summary>
        /// Добавление параметров для работы с методом
        /// </summary>
        /// <param name="parametrs">Параметры из запроса</param>
        /// <param name="payload">Параметры из тела запроса</param>
        public RequestCreatedFactory AddParametrRequest(string? parametrs, object? payload= null)
        {
            this._parametrs = parametrs;
            this._payload = payload;

            return this;
        }

        /// <summary>
        /// Проверка авторизации
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Сервис валидации не найден</exception>
        /// <exception cref="TokenValidateException">Ошибка авторизации</exception>
        public RequestCreatedFactory IsAuthorization()
        {
            if (this._endpointConfig.IsAuthorization == false)
                return this;

            string? serverceUrl = string.Empty;
            EndpointConfig enpointValidateToken;

            if (_microservicesConfig.Services.TryGetValue(TokenValidateServiceName, out var serviceConfig) &&
               serviceConfig.Endpoints.TryGetValue(TokenValidateActionName, out var endpointConfig))
            {
                enpointValidateToken = endpointConfig;
                serverceUrl = serviceConfig.BaseUrl + "/" + enpointValidateToken.Path;
            }
            else
            {
                throw new ArgumentException("Сервис валидации токена не найден");
            }

            var token = _httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
                throw new TokenValidateException("Ошибка авторизации: токен не найден");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = _httpClient?.GetAsync(serverceUrl).GetAwaiter().GetResult();
            

            if (!response.IsSuccessStatusCode)
                throw new TokenValidateException("Ошибка авторизации: Токен не найден или не валидный");

            if (_endpointConfig.Roles.Any())
            {
                var responseConten = response?.Content.ReadAsStringAsync().Result;
                var roles = JsonSerializer.Deserialize<List<string>>(responseConten);

                if(!_endpointConfig.Roles.Any(w => roles.Contains(w)))
                    throw new TokenValidateException("Ошибка авторизации: не достаточно прав");
            }            

            return this;
        }

        /// <summary>
        /// Билд обекта запроса
        /// </summary>
        /// <param name="httpMethod">Метод запроса http</param>
        /// <param name="httpClient">Http client для отправки последующего запроса</param>
        /// <param name="parameters">Параметры query запроса</param>
        /// <param name="payload">Данные из тела запроса</param>
        /// <param name="httpContext">Контекст полученого запроса</param>
        /// <returns>Обект IRequest</returns>
        /// <exception cref="ArgumentNullException">Обект типа Irequst не найден</exception>
        public DefaultRequestAbstract GetRequest(HttpMethod? httpMethod)
        {
            string requestName = GetRequstName(httpMethod);

            Type? requstType = Type.GetType(requestName);
            if (requstType != null)
            {
                var instance = Activator.CreateInstance(requstType);
                var requst = instance as DefaultRequestAbstract;
                if (requst != null)
                {
                    return requst.GetRequest(_httpClient,_parametrs, _payload, _serviceUrl, _httpContext);
                }
            }

            throw new ArgumentNullException("Тип запроса не найден");
        }

        /// <summary>
        /// Получить имя обекта IRequst
        /// </summary>
        /// <param name="endpointConfig"></param>
        /// <param name="method">Тип Http Метода</param>
        /// <returns>Назвение сервиса</returns>
        /// <exception cref="ArgumentException">IRequest не найден или метод не поддерживается</exception>
        private string GetRequstName(HttpMethod? method = null)
        {
            const string nameSpaceRequst = "ApiGateway.ApiHost.Requests.";
            string? requstName = null;

            if (!string.IsNullOrEmpty(_endpointConfig.RequstName))
            {
                requstName = _endpointConfig.RequstName;
            }


            if (!string.IsNullOrEmpty(requstName))
            {
                return nameSpaceRequst + requstName;
            }

            if (_endpointConfig.Type == "http")
            {
                switch (method?.Method)
                {
                    case "GET":
                        return nameSpaceRequst + "DefaultRequestGet";
                    case "POST":
                        return nameSpaceRequst + "DefaultRequestPost";
                    case "PUT":
                        return nameSpaceRequst + "DefaultRequestPut";
                    case "DELETE":
                        return nameSpaceRequst + "DefaultRequestDelete";
                    default:
                        throw new ArgumentException("Запрос не найден или метод не поддерживается");
                }
            }
            else if (_endpointConfig.Type == "rabit") 
            {
                throw new ArgumentException("метод не поддерживается");
            }
            else
            {
                throw new ArgumentException("метод не поддерживается");
            }
        }
    }
}