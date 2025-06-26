using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Senha { get; set; }
        public required string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
