using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Models;

namespace PaymentService.Application.UseCases
{
    public class UsuarioUseCase(IUsuarioRepository usuarioRepository) : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;

        public async Task<ApiResponse> CadastraUsuario(CadastroUsuarioDTO cadastroUsuarioDTO)
        {
            try
            {
                var Usuario = new Usuario
                {
                    Nome = cadastroUsuarioDTO.Nome,
                    Senha = cadastroUsuarioDTO.Senha,
                    Email = cadastroUsuarioDTO.Email,
                    DataCadastro = DateTime.Now,
                    TipoUsuario = cadastroUsuarioDTO.TipoUsuario
                };

                var retorno = await _usuarioRepository.AddUsuario(Usuario);

                if (retorno)
                {
                    return new ApiResponse
                    {
                        Success = true,
                        Data = true
                    };
                }

                return new ApiResponse
                {
                    Success = false,
                    Data = "Erro ao cadastrar usuário",
                    Error = "Não foi possível cadastrar o usuário no repositório"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse{
                    Success = false,
                    Data = "Erro ao cadastrar usuário",
                    Error = $"Exception: {ex.Message}, {ex.StackTrace}"
                };
            }
        }
    }
}
