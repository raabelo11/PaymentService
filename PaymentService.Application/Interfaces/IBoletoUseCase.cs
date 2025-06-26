using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;

namespace PaymentService.Application.Interfaces
{
    public interface IBoletoUseCase
    {
        Task<ApiResponse> GeraBoleto(BoletoDTO boletoDTO);
    }
}
