using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Data.Context;

namespace PaymentService.Infrastructure.Data.Repository
{
    public class BoletoRepository(PaymentsDbContext context) : IBoletoRepository
    {
        private readonly PaymentsDbContext _context = context;

    }
}
