using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;

namespace PaymentService.Api.Controllers
{
    /// <summary>
    ///     API Boletos
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BoletosController(IBoletoUseCase boletoUseCase) : Controller
    {
        private readonly IBoletoUseCase _boletoUseCase = boletoUseCase;

        /// <summary>
        ///     Endpoint responsável por criar os boletos
        /// </summary>
        /// <param name="boletoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CriarBoleto")]
        public async Task<ActionResult<ApiResponse>> CriarBoleto([FromBody] BoletoDTO boletoDTO)
        {
            var ret = await _boletoUseCase.GeraBoleto(boletoDTO);
            return ret.Success ? Ok(ret) : BadRequest(ret);
        }

        //Paga Boleto

        //EditaBoleto

        //ExcluiBoleto
    }
}
