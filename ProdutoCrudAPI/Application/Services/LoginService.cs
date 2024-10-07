using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProdutoCrudAPI.Application.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProdutoCrudAPI.Application.Services
{
    public class LoginService : ILoginService
    {
        public string Login(LoginDto loginDto)
        {
            if (loginDto.Usuario is null or not "root")
                return null;

            // Gerar o token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, loginDto.Usuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
