using ProdutoCrudAPI.Application.Dtos;

namespace ProdutoCrudAPI.Application.Services;
public interface IProdutoService
{
    ProdutoDto CreateProduto(ProdutoCreateDto produtoAddDto);
    ProdutoDto Update(int id, ProdutoUpdateDto produtoUpdateDto);
    void Delete(int id);
    ProdutoDto GetById(int id);
    IEnumerable<ProdutoDto> GetAll(int skip, int take, string? nome, string? descricao);
}
