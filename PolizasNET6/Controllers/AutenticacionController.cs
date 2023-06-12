using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolizasNET6.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PolizasNET6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string sKey;

        public AutenticacionController(IConfiguration config)
        {
            sKey = config.GetSection("settings").GetSection("SKey").ToString();
        }

        [HttpPost]
        [Route("auth")]
        public IActionResult auth([FromBody] Usuario request)
        {
            if(request.correo == "pruebas@gmail.com" && request.contraseña == "pruebas123")
            {
                var kBytes = Encoding.ASCII.GetBytes(sKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(45),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(kBytes), SecurityAlgorithms.HmacSha256Signature)

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string ourToken = tokenHandler.WriteToken(tokenConfig);
                return StatusCode(StatusCodes.Status200OK, new { token = ourToken });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }
    }
}
