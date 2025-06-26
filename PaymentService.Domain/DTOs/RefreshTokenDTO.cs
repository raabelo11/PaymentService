namespace PaymentService.Domain.DTOs
{
    public class RefreshTokenDTO
    {
        public int IdUsuario { get; set; }
        public Guid? RefreshToken { get; set; }
        public DateTime ExpiracaoRefreshToken { get; set; }
    }
}
