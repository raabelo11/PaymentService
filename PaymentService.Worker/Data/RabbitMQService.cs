using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using PaymentService.Worker;
using System.Text;

public class RabbitMQService
{
    private IConnection _connection;
    private IChannel _channel;
    private readonly IOptions<Config> _appsettings;

    public RabbitMQService(IOptions<Config> appsettings)
    {
        _appsettings = appsettings;
    }

    public async Task Connection()
    {
        var factory = new ConnectionFactory
        {
            HostName = _appsettings.Value.RabbitMQConfig.HostName,
            Port = _appsettings.Value.RabbitMQConfig.Port,
            UserName = _appsettings.Value.RabbitMQConfig.User,
            Password = _appsettings.Value.RabbitMQConfig.Password
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
    }

    public async Task ReceiveMessage(string queue, Action<string> onMessage)
    {
        await _channel.QueueDeclareAsync(queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            onMessage(message);
            await Task.CompletedTask;
        };

        await _channel.BasicConsumeAsync(queue, autoAck: true, consumer: consumer);
    }
}
