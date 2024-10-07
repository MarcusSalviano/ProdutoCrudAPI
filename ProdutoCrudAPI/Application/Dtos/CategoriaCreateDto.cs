using System.ComponentModel.DataAnnotations;

namespace ProdutoCrudAPI.Application.Dtos
{
    public class CategoriaCreateDto
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [MaxLength(100, ErrorMessage = "A descrição não pode exceder 100 caracteres")]
        public string Nome { get; set; }
    }
}