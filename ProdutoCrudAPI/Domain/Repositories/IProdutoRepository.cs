using ProdutoCrudAPI.Domain.Models;

namespace ProdutoCrudAPI.Domain.Repositories;
public interface IProdutoRepository
{
    void Create(Produto produto);
    void Update(Produto produto);
    void Delete(int id);
    Produto GetById(int id);
    IEnumerable<Produto> GetAll(int skip, int take, string? nome, string? descricao);
}
