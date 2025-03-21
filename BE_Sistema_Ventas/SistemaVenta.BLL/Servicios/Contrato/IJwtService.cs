using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SistemaVenta.DTO;
using Microsoft.Extensions.Configuration;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IJwtService
    {
        string GenerarToken(SesionDTO usuario);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;

        public JwtService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(SesionDTO usuario)
        {
            // Clave secreta
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            // Claims (información del usuario)
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Correo),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("IdUsuario", usuario.IdUsuario.ToString()),
            new Claim("Rol", usuario.RolDescripcion) // Agrega información adicional si es necesario
            };

            // Generar credenciales de firma
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token válido por 1 hora
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}