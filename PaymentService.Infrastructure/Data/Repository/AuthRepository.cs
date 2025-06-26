using PaymentService.Domain.Interfaces;
using PaymentService.Infrastructure.Data.Context;

namespace PaymentService.Infrastructure.Data.Repository
{
    public class AuthRepository(PaymentsDbContext context) : IAuthRepository
    {
        private readonly PaymentsDbContext _context = context;


    }
}
