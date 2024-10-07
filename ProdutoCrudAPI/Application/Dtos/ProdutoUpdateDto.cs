namespace ProdutoCrudAPI.Application.Dtos;
public class ProdutoUpdateDto
{
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double? Preco { get; set; }
    public DateTime? DataValidade { get; set; }
    public string? Imagem { get; set; }
    public int IdCategoria { get; set; }
    public CategoriaUpdateDto Categoria { get; set; }
}
