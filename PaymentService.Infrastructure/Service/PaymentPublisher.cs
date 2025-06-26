using Microsoft.Extensions.Options;
using PaymentService.Domain.General;
using PaymentService.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace PaymentService.Infrastructure.Services
{
    public class PaymentPublisher(IOptions<Config> appsettings) : IPaymentPublisher
    {
        private readonly IOptions<Config> _appsettings = appsettings;

        public async Task<bool> SendQueue(string queue, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = _appsettings.Value.RabbitMQConfig.HostName,
                Port = _appsettings.Value.RabbitMQConfig.Port,
                UserName = _appsettings.Value.RabbitMQConfig.User,
                Password = _appsettings.Value.RabbitMQConfig.Password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queue, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queue, body: body);

            return true;
        }
    }
}