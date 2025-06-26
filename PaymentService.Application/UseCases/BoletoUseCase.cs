using System.Text.Json;
using Microsoft.Extensions.Options;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Domain.ReturnValue;

namespace PaymentService.Application.UseCases
{
    public class BoletoUseCase(IBoletoRepository boletoRepository, IPaymentPublisher publisher, IOptions<Config> appsettings) : IBoletoUseCase
    {
        private readonly IBoletoRepository _boletoRepository = boletoRepository;
        private readonly IPaymentPublisher _publisher = publisher;
        private readonly IOptions<Config> _appsettings = appsettings;

        public async Task<ApiResponse> GeraBoleto(BoletoDTO boletoDTO)
        {
            try
            {
                BoletoCriadoReturnValue boletoReturnValue = new BoletoCriadoReturnValue();

                var boleto = new Boleto()
                {
                    IdentificadorBoleto = Guid.NewGuid(),
                    DataVencimento = boletoDTO.DataVencimento,
                    Valor = boletoDTO.Valor,
                    NomePagador = boletoDTO.NomePagador,
                    NomeRecebedor = boletoDTO.NomeRecebedor,
                    Status = Domain.Enums.StatusBoleto.Gerado,
                    CodBanco = boletoDTO.CodBanco
                };

                // Manda o boleto pra fila
                var json = JsonSerializer.Serialize(boleto);
                bool send = await _publisher.SendQueue(_appsettings.Value.RabbitMQConfig.Queues.BoletoEnviado, json);

                if (!send)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Error = "Erro ao enviar para fila."
                    };
                }

                boletoReturnValue.IdentificadorBoleto = boleto.IdentificadorBoleto;

                return new ApiResponse
                {
                    Success = true,
                    Data = boletoReturnValue
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = true,
                    Error = $"Exception: {ex.Message}, {ex.StackTrace}"
                };
            }
        }
    }
}


//// Cria boleto no banco
//if (await _boletoRepository.AddBoleto(boleto))
//{
//    HistBoleto histBoleto = new HistBoleto()
//    {
//        IdBoletoHist = boleto.IdBoleto,
//        DataHoraStatus = DateTime.Now,
//        StatusBoleto = boleto.Status
//    };

//    // Salva histórico do status do boleto
//    await _boletoRepository.AddHistBoleto(histBoleto);


//    return new ApiResponse
//    {
//        Success = true,
//        Data = boletoReturnValue
//    };
//}
