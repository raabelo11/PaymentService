using PaymentService.Domain.Enums;

namespace PaymentService.Domain.DTOs
{
    public class CadastroUsuarioDTO
    {
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public required string Email { get; set; }
        public required TipoUsuario TipoUsuario { get; set; }
    }
}
