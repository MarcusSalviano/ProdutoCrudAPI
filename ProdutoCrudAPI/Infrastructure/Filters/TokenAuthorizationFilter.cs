using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProdutoCrudAPI.Infrastructure.Filters;
public class TokenAuthorizationFilter : IAuthorizationFilter
{
    private readonly string _secretKey;

    public TokenAuthorizationFilter(string secretKey)
    {
        _secretKey = secretKey;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Verifica se a ação atual é o método Login no LoginController
        var actionName = context.ActionDescriptor.RouteValues["action"];
        var controllerName = context.ActionDescriptor.RouteValues["controller"];

        if (controllerName == "Login" && actionName == "Login")
        {
            return; // Ignora a validação de token para o método Login
        }

        // Extrai o token do cabeçalho Authorization
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            // Se o token estiver ausente, retorna 401 Unauthorized
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {
            // Configura o validador de tokens
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // Outras validações, como validade do token
            }, out SecurityToken validatedToken);

            // Se a validação passou, o fluxo continua normalmente
        }
        catch
        {
            // Se o token for inválido, retorna 401 Unauthorized
            context.Result = new UnauthorizedResult();
        }
    }
}
