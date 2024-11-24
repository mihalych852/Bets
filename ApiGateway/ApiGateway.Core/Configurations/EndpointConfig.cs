namespace ApiGateway_Core.Configurations
{
    public class EndpointConfig
    {
        public string Type { get; set; } = "Http"; // Тип может быть Http или RabbitMQ
        public string? Path { get; set; } // Используется для Http
        public string? Queue { get; set; } // Используется для RabbitMQ
        public string? RequstName { get; set; } // используется для кастомного реквеста
        public bool IsAuthorization { get; set; } = false; // используется для проверки авторизации
        public string[] Roles { get; set; } = []; // используется для роллей
    }
}