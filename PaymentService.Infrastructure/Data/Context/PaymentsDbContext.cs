using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Data.Configuration;

namespace PaymentService.Infrastructure.Data.Context
{
    public class PaymentsDbContext : DbContext
    {
        public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
           : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Boleto> Boletos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new BoletoConfiguration());
        }
    }
}
