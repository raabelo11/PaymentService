using PaymentService.Domain.DTOs;
using PaymentService.Domain.Models;

namespace PaymentService.Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        Task<ApiResponse> CadastraUsuario(CadastroUsuarioDTO cadastroUsuarioDTO);
    }
}
