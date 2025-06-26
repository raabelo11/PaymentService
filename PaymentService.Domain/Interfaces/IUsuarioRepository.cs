using PaymentService.Domain.Models;

namespace PaymentService.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> AddUsuario(Usuario usuario);
    }
}
