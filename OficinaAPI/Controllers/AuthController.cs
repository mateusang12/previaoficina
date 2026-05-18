using Microsoft.AspNetCore.Mvc;
using OficinaAPI.DTOs;

namespace OficinaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Rota: POST api/auth/login
        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginDto loginDto)
        {
            // Validação fictícia do admin para testes do Front-end
            if (loginDto.Email == "admin@oficina.com" && loginDto.Senha == "admin123")
            {
                return Ok(new { token = "token-ficticio-valido-admin", message = "Login realizado com sucesso!" });
            }

            return Unauthorized(new { message = "E-mail ou senha inválidos." });
        }
    }
}