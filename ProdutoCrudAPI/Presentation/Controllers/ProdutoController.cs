using Microsoft.AspNetCore.Mvc;
using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Application.Services;

namespace ProdutoCrudAPI.Presentation.Controllers;

[Route("api/produtos")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtosService;

    public ProdutoController(IProdutoService produtosService)
    {
        _produtosService = produtosService;
    }

    [HttpGet]
    public IEnumerable<ProdutoDto> GetProdutos([FromQuery] int? skip, [FromQuery] int? take, [FromQuery] string? nome, [FromQuery] string? descricao)
    {
        return _produtosService.GetAll(skip ?? 0, take ?? 50, nome, descricao);
    }

    [HttpGet("{id}")]
    public IActionResult GetProdutoById(int id)
    {        
        var produto = _produtosService.GetById(id);
        if (produto == null) return NotFound();        

        return Ok(produto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult CreateProduto([FromBody] ProdutoCreateDto produtoDto)
    {
        var produto = _produtosService.CreateProduto(produtoDto);
        return CreatedAtAction(nameof(GetProdutoById), new { id = produto.Id }, produto);
    }


    [HttpDelete("{id}")]
    public IActionResult DeletarProdutoPorId(int id)
    {
        _produtosService.Delete(id);

        return NoContent(); // Retorna 204 No Content após a deleção
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarProduto(int id, [FromBody] ProdutoUpdateDto produtoUpdateDto)
    {               
        var produto = _produtosService.Update(id, produtoUpdateDto);

        // Se não encontrar, retorna 404
        if (produto == null) return NotFound();
        
        return Ok(produto);
    }
}
