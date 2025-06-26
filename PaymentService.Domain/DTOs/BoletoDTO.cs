using System.ComponentModel.DataAnnotations;

namespace PaymentService.Domain.DTOs
{
    public class BoletoDTO
    {
        [Required]
        public DateTime DataVencimento { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [Required]
        [MaxLength(50)]
        public required string NomePagador { get; set; }

        [Required]
        [MaxLength(50)]
        public required string NomeRecebedor { get; set; }

        [Required]
        public int CodBanco { get; set; }
    }
}
