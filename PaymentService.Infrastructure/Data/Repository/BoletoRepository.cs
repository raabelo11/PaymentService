using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Data.Context;

namespace PaymentService.Infrastructure.Data.Repository
{
    public class BoletoRepository(PaymentsDbContext context) : IBoletoRepository
    {
        private readonly PaymentsDbContext _context = context;

        public async Task<bool> AddBoleto(Boleto boleto)
        {
            await _context.Boletos.AddAsync(boleto);

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> AddHistBoleto(HistBoleto histBoleto)
        {
            await _context.HistBoletos.AddAsync(histBoleto);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
