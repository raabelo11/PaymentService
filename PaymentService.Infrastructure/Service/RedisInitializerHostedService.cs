using Microsoft.Extensions.Hosting;
using PaymentService.Domain.Interfaces;

namespace PaymentService.Infrastructure.Service
{
    public class RedisInitializerHostedService(IRedisService redisService) : IHostedService
    {
        private readonly IRedisService _redisService = redisService;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _redisService.InicializaListaRefreshToken();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
