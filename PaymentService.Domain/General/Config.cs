namespace PaymentService.Domain.General
{
    public class Config
    {
        public required RabbitMQConfig RabbitMQConfig { get; set; }
        public required Redis Redis { get; set; }
    }

    public class RabbitMQConfig
    {
        public required string HostName { get; set; }
        public string RoutingKey { get; set; }
        public int Port { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
        public required string VirtualHost { get; set; }
        public required Queues Queues { get; set; }
    }
    
    public class Queues
    {
        public required string BoletoEnviado { get; set; }
    }

    public class Redis
    {
        public string Url { get; set; }
    }
}
