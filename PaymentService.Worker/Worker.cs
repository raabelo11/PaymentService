namespace PaymentService.Worker
{
    public class Worker(RabbitMQService boletosRead) : BackgroundService
    {
        private readonly RabbitMQService _boletosRead = boletosRead;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _boletosRead.Connection();
            await _boletosRead.ReceiveMessage("boleto.enviado.queue", async mensagem =>
            {
                Console.WriteLine(mensagem);
                await Task.CompletedTask;
            });
        }
    }
}
