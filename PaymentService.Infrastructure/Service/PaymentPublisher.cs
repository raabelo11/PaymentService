using RabbitMQ.Client;
using System.Text;

namespace PaymentService.Infrastructure.Services
{
    public class PaymentPublisher
    {
        public async Task<bool> SendQueue()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            const string message = "Hello World!";
            var body = Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);

            return true;
        }
    }
}
