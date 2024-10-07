using System.ComponentModel.DataAnnotations;

namespace ProdutoCrudAPI.Application.Dtos;
public class ProdutoCreateDto
{
    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [MaxLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [MaxLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório")]
    public double Preco { get; set; }

    [Required(ErrorMessage = "A data de validade é obrigatória")]
    [DataValidadeValidation(ErrorMessage = "A data de validade deve ser maior que a data atual.")]
    public DateTime DataValidade { get; set; }

    [Required(ErrorMessage = "A caminho da imagem é obrigatório")]
    public string Imagem { get; set; }

    [Required]
    public CategoriaCreateDto Categoria { get; set; }
}
