using System.Text.Json;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.Interfaces;
using StackExchange.Redis;

namespace PaymentService.Infrastructure.Service
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _db;

        public RedisService(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }

        public async Task AddCache<T>(string chave, List<T> lista, TimeSpan? expiracao = null)
        {
            var json = JsonSerializer.Serialize(lista);
            await _db.StringSetAsync(chave, json, expiracao);
        }

        public async Task<List<T>?> GetListCache<T>(string chave)
        {
            var json = await _db.StringGetAsync(chave);
            if (json.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<List<T>>(json!);
        }

        public async Task InicializaListaRefreshToken()
        {
            var lista = new List<RefreshTokenDTO>();
            var json = JsonSerializer.Serialize(lista);

            await _db.StringSetAsync("ListRefreshToken", json);
        }
    }
}
