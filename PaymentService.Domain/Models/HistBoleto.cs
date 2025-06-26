using PaymentService.Domain.Enums;

namespace PaymentService.Domain.Models
{
    public class HistBoleto
    {
        public long IdBoletoHist { get; set; }
        public DateTime DataHoraStatus { get; set; }
        public StatusBoleto StatusBoleto { get;  set; }
    }
}
