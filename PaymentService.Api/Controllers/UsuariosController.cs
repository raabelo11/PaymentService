using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.Models;

namespace PaymentService.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController(IUsuarioUseCase usuarioUseCase) : Controller
    {
        private readonly IUsuarioUseCase _usuarioUseCase = usuarioUseCase;


        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult<ApiResponse>> Cadastrar(CadastroUsuarioDTO cadastroUsuarioDTO)
        {
            var ret = await _usuarioUseCase.CadastraUsuario(cadastroUsuarioDTO);
            return ret.Success ? Ok(ret) : BadRequest(ret);
        }

        //edita usuario

        //exclui usuario
    }
}
