using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Data.Context;

namespace PaymentService.Infrastructure.Data.Repository
{
    public class UsuarioRepository(PaymentsDbContext context) : IUsuarioRepository
    {
        private readonly PaymentsDbContext _context = context;

        public async Task<bool> AddUsuario(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
