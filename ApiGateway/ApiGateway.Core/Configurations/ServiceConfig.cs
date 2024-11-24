namespace ApiGateway_Core.Configurations
{
    public class ServiceConfig
    {
        public string BaseUrl { get; set; }
        public Dictionary<string, EndpointConfig> Endpoints { get; set; }
    }
}