using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;

namespace PaymentService.Api.Controllers
{
    /// <summary>
    ///     API Usuarios
    /// </summary>
    /// <param name="usuarioUseCase"></param>
    /// <param name="authService"></param>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsuariosController(IUsuarioUseCase usuarioUseCase, IAuthService authService) : Controller
    {
        private readonly IUsuarioUseCase _usuarioUseCase = usuarioUseCase;
        private readonly IAuthService _authService = authService;

        /// <summary>
        ///     Endpoint de cadastro de usuários
        /// </summary>
        /// <param name="cadastroUsuarioDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Cadastrar")]
        public async Task<ActionResult<ApiResponse>> Cadastrar([FromBody] CadastroUsuarioDTO cadastroUsuarioDTO)
        {
            var ret = await _usuarioUseCase.CadastraUsuario(cadastroUsuarioDTO);
            return ret.Success ? StatusCode(201, ret) : BadRequest(ret);
        }

        /// <summary>
        ///     Endpoint de login de usuarios cadastrados
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginDTO loginDTO)
        {
            var ret = await _authService.Login(loginDTO);
            return ret.Success ? Ok(ret) : BadRequest(ret);
        }

        /// <summary>
        ///     Endpoint responsável por gerar um novo token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("RefreshToken")]
        public async Task<ActionResult<ApiResponse>> RefreshToken(Guid refreshToken)
        {
            var ret = await _authService.GerarToken(null, refreshToken);
            if (ret is null) return Unauthorized();
            return Ok(ret);
        }

        /// <summary>
        ///     Endpoint auxiliar responsável por identificar se usuário está ou não autenticado
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("Autenticado")]
        public ActionResult<bool> Autenticado()
        {
            return Ok(true);
        }
    }
}