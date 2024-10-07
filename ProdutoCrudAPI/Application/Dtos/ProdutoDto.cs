namespace ProdutoCrudAPI.Application.Dtos;
public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public DateTime DataValidade { get; set; }
    public string Imagem { get; set; }
    public CategoriaDto Categoria { get; set; }
}
