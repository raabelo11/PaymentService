using PaymentService.Domain.General;

namespace PaymentService.Domain.ReturnValue
{
    public class LoginReturnValue
    {
        public int IdUsuario { get; set; }
        public object? Token { get; set; }
        public Guid? RefreshToken { get; set; }
    }
}
