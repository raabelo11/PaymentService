using System.ComponentModel.DataAnnotations;
using PaymentService.Domain.Enums;

namespace PaymentService.Domain.DTOs
{
    public class CadastroUsuarioDTO
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Senha { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$", ErrorMessage = "E-mail inválido")]
        public required string Email { get; set; }

        public required TipoUsuario TipoUsuario { get; set; }
    }
}
