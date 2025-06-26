using Microsoft.EntityFrameworkCore;
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

        public async Task<Usuario?> GetUsuarioByEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email && !p.Excluido);
        }

        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && !p.Excluido);
        }
    }
}
