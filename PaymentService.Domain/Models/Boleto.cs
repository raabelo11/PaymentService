using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Models
{
    public class Boleto
    {
        public long IdBoleto { get; set; }
        public Guid IdentificadorBoleto { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal Valor { get; set; }
        public required string NomePagador { get; set; }
        public required string NomeRecebedor { get; set; }
        public StatusPagamento Status { get; set; }
        public int CodBanco { get; set; }
    }
}
