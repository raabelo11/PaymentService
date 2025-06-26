using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;
using PaymentService.Domain.Models;
using PaymentService.Domain.ReturnValue;

namespace PaymentService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> Login(LoginDTO loginDTO);
        Task<LoginReturnValue> GerarToken(Usuario? usuario, Guid? refreshToken = null);
    }
}
