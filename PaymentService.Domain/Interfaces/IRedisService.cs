namespace PaymentService.Domain.Interfaces
{
    public interface IRedisService
    {
        Task AddCache<T>(string chave, List<T> lista, TimeSpan? expiracao = null);
        Task<List<T>?> GetListCache<T>(string chave);
        Task InicializaListaRefreshToken();
    }
}
