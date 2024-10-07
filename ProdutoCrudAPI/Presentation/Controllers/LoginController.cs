using Microsoft.AspNetCore.Mvc;
using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Application.Services;

namespace ProdutoCrudAPI.Presentation.Controllers;

[Route("login")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var tokenString = _loginService.Login(loginDto);

        if (tokenString is null)
            return Unauthorized(new { message = "Credenciais inválidas." });

        return Ok(new { Token = tokenString });
    }
}
