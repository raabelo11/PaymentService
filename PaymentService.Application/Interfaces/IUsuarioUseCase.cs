using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;

namespace PaymentService.Application.Interfaces
{
    public interface IUsuarioUseCase
    {
        Task<ApiResponse> CadastraUsuario(CadastroUsuarioDTO cadastroUsuarioDTO);
    }
}
