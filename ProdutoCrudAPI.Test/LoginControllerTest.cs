using Microsoft.AspNetCore.Mvc.Testing;
using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Presentation.Controllers;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace ProdutoCrudAPI.Test;
public class LoginControllerTest : IClassFixture<WebApplicationFactory<LoginController>>, IClassFixture<WebApplicationFactory<ProdutoController>>
{
    private readonly HttpClient _login;
    private readonly HttpClient _produto;

    public LoginControllerTest(WebApplicationFactory<LoginController> loginFactory, WebApplicationFactory<ProdutoController> produtoFactory)
    {
        _login = loginFactory.CreateClient();
        _produto = produtoFactory.CreateClient();
    }

    [Fact]
    public async Task PostLogin_ReturnsOkResponse_WhenValidLogin()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Usuario = "root"
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var requestUrl = "/login";

        // Act
        var response = await _login.PostAsync(requestUrl, jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("root1")]
    [InlineData("1root")]
    public async Task PostLogin_ReturnsUnauthorizedResponse_WhenInvalidLogin(string usuario)
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Usuario = usuario
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
        var requestUrl = "/login";

        // Act
        var response = await _login.PostAsync(requestUrl, jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ReturnsOKResponse_WhenValidTokenIsPassed()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Usuario = "root"
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

        // Act
        var responseLogin = await _login.PostAsync("/login", jsonContent);
        var responseLoginString = await responseLogin.Content.ReadAsStringAsync();

        var jsonResponse = JsonSerializer.Deserialize<LoginResponse>(responseLoginString, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var token = jsonResponse.Token;

        _produto.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _produto.GetAsync("/api/produtos");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("root", "api/produtos")]
    [InlineData("root", "/api/produtos/1")]
    public async Task Get_ReturnsUnauthorizedResponse_WhenValidTokenIsNotPassed(string usuario, string requestUrl)
    {
        // Arrange
        _produto.DefaultRequestHeaders.Authorization = null;

        // Act
        var response = await _produto.GetAsync(requestUrl);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    internal class LoginResponse
    {
        public string Token { get; set; }
    }
}
