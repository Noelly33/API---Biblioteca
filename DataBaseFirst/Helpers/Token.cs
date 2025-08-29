using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Helpers
{
    public class Token
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(UsuarioRolDto usuario, List<Menu> permiso) { 
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.id_usuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.usuario ?? ""),
                new Claim(ClaimTypes.Email, usuario.correo ?? ""),
                new Claim(ClaimTypes.Role, usuario.nombre_rol ?? "")
            };

            foreach (var menu in permiso) 
            {
                claims.Add(new Claim("permisos", menu.UrlMenu ?? ""));
            }
             var credencial = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtSettings.GetValue<int>("DurationInMinutes")),
                signingCredentials: credencial
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
