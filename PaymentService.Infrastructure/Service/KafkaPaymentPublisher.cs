using Confluent.Kafka;
using System.Text.Json;

namespace PaymentService.Infrastructure.Service
{
    public class KafkaPaymentPublisher
    {
        private ProducerConfig _configSend;
        private ConsumerConfig _configReceive;
        private Random _random = new Random();

        public KafkaPaymentPublisher()
        {
            _configSend = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _configReceive = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "grupo-local",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        public IProducer<Null, string> CreateProducer()
        {
            return new ProducerBuilder<Null, string>(_configSend).Build();
        }

        public async Task Send()
        {
            var mensagem = new
            {
                Id = _random.Next(1, 2000),
                Texto = $"[{_random.Next(1, 2000)}] - TesteJSON"
            };

            var json = JsonSerializer.Serialize(mensagem);

            var producer = this.CreateProducer();

            await producer.ProduceAsync("topic.1", new Message<Null, string> { Value = json });

            Console.WriteLine("Mensagem publicada com sucesso!");
        }

        public void Receive()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(_configReceive).Build();

            consumer.Subscribe("topic.1");

            Console.WriteLine("Consumidor iniciado.....");

            try
            {
                while (true)
                {
                    var resultado = consumer.Consume(CancellationToken.None);
                    Console.WriteLine($"Mensagem recebida: {resultado.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
