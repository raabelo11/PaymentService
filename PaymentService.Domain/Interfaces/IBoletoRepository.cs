using PaymentService.Domain.Models;

namespace PaymentService.Domain.Interfaces
{
    public interface IBoletoRepository
    {
        Task<bool> AddBoleto(Boleto boleto);
        Task<bool> AddHistBoleto(HistBoleto histBoleto);
    }
}
