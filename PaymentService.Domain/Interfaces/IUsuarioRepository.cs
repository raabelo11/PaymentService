using PaymentService.Domain.Models;

namespace PaymentService.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> AddUsuario(Usuario usuario);
        Task<Usuario?> GetUsuarioByEmail(string email);
        Task<Usuario?> GetUsuarioById(int id);
    }
}
