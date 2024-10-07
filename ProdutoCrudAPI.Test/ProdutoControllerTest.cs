using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Application.Services;
using ProdutoCrudAPI.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ProdutoCrudAPI.Test;
public class ProdutoControllerTest
{
    private readonly Mock<IProdutoService> _produtoServiceMock;
    private readonly ProdutoController _controller;

    public ProdutoControllerTest()
    {
        _produtoServiceMock = new Mock<IProdutoService>();
        _controller = new ProdutoController(_produtoServiceMock.Object);
    }

    [Fact]
    public void GetProdutos_Returns_Produtos_List()
    {
        // Arrange
        var produtosList = new List<ProdutoDto>
        {
            new ProdutoDto { Id = 1, Nome = "Produto 1", Descricao = "Descricao 1", Preco = 10.0 },
            new ProdutoDto { Id = 2, Nome = "Produto 2", Descricao = "Descricao 2", Preco = 20.0 }
        };

        _produtoServiceMock.Setup(service => service.GetAll(It.IsAny<int>(), It.IsAny<int>(), null, null))
                           .Returns(produtosList);

        // Act
        var result = _controller.GetProdutos(null, null, null, null);

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetProdutoById_Returns_Ok_When_Produto_Exists()
    {
        // Arrange
        var produto = new ProdutoDto { Id = 1, Nome = "Produto 1", Descricao = "Descricao 1", Preco = 10.0 };
        _produtoServiceMock.Setup(service => service.GetById(1)).Returns(produto);

        // Act
        var result = _controller.GetProdutoById(1) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(produto, result.Value);
    }

    [Fact]
    public void GetProdutoById_Returns_NotFound_When_Produto_Does_Not_Exist()
    {
        // Arrange
        _produtoServiceMock.Setup(service => service.GetById(1)).Returns((ProdutoDto)null);

        // Act
        var result = _controller.GetProdutoById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void CreateProduto_Returns_CreatedAtAction()
    {
        // Arrange
        var produtoCreateDto = new ProdutoCreateDto { Nome = "Produto Novo", Descricao = "Descricao", Preco = 10.0, Categoria = new CategoriaCreateDto { Nome = "Categoria" } };
        var produtoDto = new ProdutoDto { Id = 1, Nome = "Produto Novo", Descricao = "Descricao", Preco = 10.0 };

        _produtoServiceMock.Setup(service => service.CreateProduto(produtoCreateDto)).Returns(produtoDto);

        // Act
        var result = _controller.CreateProduto(produtoCreateDto) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(_controller.GetProdutoById), result.ActionName);
        Assert.Equal(1, result.RouteValues["id"]);
        Assert.Equal(produtoDto, result.Value);
    }

    [Fact]
    public void DeletarProdutoPorId_Returns_NoContent()
    {
        // Arrange
        _produtoServiceMock.Setup(service => service.Delete(It.IsAny<int>()));

        // Act
        var result = _controller.DeletarProdutoPorId(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void AtualizarProduto_Returns_Ok_When_Produto_Exists()
    {
        // Arrange
        var produtoUpdateDto = new ProdutoUpdateDto { Nome = "Produto Atualizado", Descricao = "Descricao Atualizada" };
        var produtoDto = new ProdutoDto { Id = 1, Nome = "Produto Atualizado", Descricao = "Descricao Atualizada" };

        _produtoServiceMock.Setup(service => service.Update(1, produtoUpdateDto)).Returns(produtoDto);

        // Act
        var result = _controller.AtualizarProduto(1, produtoUpdateDto) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(produtoDto, result.Value);
    }

    [Fact]
    public void AtualizarProduto_Returns_NotFound_When_Produto_Does_Not_Exist()
    {
        // Arrange
        var produtoUpdateDto = new ProdutoUpdateDto { Nome = "Produto Atualizado", Descricao = "Descricao Atualizada" };

        _produtoServiceMock.Setup(service => service.Update(1, produtoUpdateDto)).Returns((ProdutoDto)null);

        // Act
        var result = _controller.AtualizarProduto(1, produtoUpdateDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
