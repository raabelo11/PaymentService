namespace PaymentService.Api.Configuration
{
    public class Config
    {
        public required RabbitMQConfig RabbitMQConfig { get; set; }
    }

    public class RabbitMQConfig
    {
        public required string HostName { get; set; }
        public required string Queue { get; set; }
    }
}
