using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentService.Application.Interfaces;
using PaymentService.Domain.DTOs;
using PaymentService.Domain.General;
using PaymentService.Domain.Interfaces;
using PaymentService.Domain.Models;
using PaymentService.Domain.ReturnValue;

namespace PaymentService.Application.Service
{
    public class AuthService(IUsuarioRepository usuarioRepository, IConfiguration appsettings, IRedisService cache) : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly IConfiguration _appsettings = appsettings;
        private readonly IRedisService _cache = cache;

        public async Task<ApiResponse> Login(LoginDTO loginDTO)
        {
			try
			{
                var retorno = new ApiResponse();
                retorno.Success = false;

                var usuario = await _usuarioRepository.GetUsuarioByEmail(loginDTO.Email);

                if (usuario == null)
                {
                    retorno.Error = "Usuário ou senha inválidos !";
                    return retorno;
                }

                if (!BCrypt.Net.BCrypt.Verify(loginDTO.Senha, usuario.Senha))
                {
                    retorno.Error = "Usuário ou senha inválidos !";
                    return retorno;
                }

                var token = await this.GerarToken(usuario);
                if (token is null)
                {
                    retorno.Error = "Erro na geração do Token !";
                    return retorno;
                }

                Guid guidToken = Guid.NewGuid();
                token.RefreshToken = guidToken;

                var listRefreshToken = await _cache.GetListCache<RefreshTokenDTO>("ListRefreshToken");

                listRefreshToken?.Add(new RefreshTokenDTO()
                {
                    IdUsuario = usuario.Id,
                    RefreshToken = guidToken,
                    ExpiracaoRefreshToken = DateTime.Now.AddDays(5)
                });

                var salvaCacheAtualizado = JsonSerializer.Serialize(listRefreshToken);

                if (listRefreshToken is not null)
                    await _cache.AddCache("ListRefreshToken", listRefreshToken);

                return new ApiResponse
                {
                    Success = true,
                    Data = token
                };
			}
			catch (Exception ex)
			{
                return new ApiResponse
                {
                    Success = false,
                    Data = "Erro na autenticação !",
                    Error = $"Exception: {ex.Message}, {ex.StackTrace}"
                };
			}
        }

        public async Task<LoginReturnValue> GerarToken(Usuario? usuario, Guid? refreshToken = null)
        {
            Usuario user = new Usuario();
            var retorno = new LoginReturnValue();
            List<RefreshTokenDTO> listaCache = new List<RefreshTokenDTO>();
            Guid newRefreshToken = Guid.NewGuid();

            if (refreshToken is not null)
            {
                var cache = await _cache.GetListCache<RefreshTokenDTO>("ListRefreshToken");

                if (cache is null)
                    return null;

                var findCache = cache.FirstOrDefault(p => p.RefreshToken == refreshToken);

                if (findCache is null)
                    return null;

                if (findCache.ExpiracaoRefreshToken < DateTime.Now)
                    return null;

                var usuarioRefreshToken = await _usuarioRepository.GetUsuarioById(findCache.IdUsuario);

                if (usuarioRefreshToken is not null)
                {
                    user = usuarioRefreshToken;
                    findCache.RefreshToken = newRefreshToken;
                    findCache.ExpiracaoRefreshToken = DateTime.Now.AddDays(5);

                    listaCache = cache;
                }
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings["Jwt:Key"] ?? ""));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, (refreshToken is null) ? usuario.Id.ToString() : user.Id.ToString()),
                new Claim(ClaimTypes.Name, (refreshToken is null) ? usuario.Nome : user.Nome),
                new Claim(ClaimTypes.Email, (refreshToken is null) ? usuario.Email : user.Email),
                new Claim(ClaimTypes.Role, (refreshToken is null) ? usuario.TipoUsuario.ToString() : user.TipoUsuario.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _appsettings["Jwt:Issuer"] ?? "",
                audience: _appsettings["Jwt:Audience"] ?? "",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_appsettings["Jwt:ExpireMinutes"] ?? "")),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            if (!tokenString.IsNullOrEmpty())
                await _cache.AddCache("ListRefreshToken", listaCache);

            retorno.IdUsuario = user.Id;
            retorno.Token = tokenString;
            retorno.RefreshToken = (refreshToken is null) ? null : newRefreshToken;
            
            return retorno;
        }
    }
}
