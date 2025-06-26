using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Excluido { get; set; }
    }
}
