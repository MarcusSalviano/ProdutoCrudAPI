using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProdutoCrudAPI.Domain.Models;
public class Categoria
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [JsonIgnore]
    public virtual ICollection<Produto>? Produtos { get; set; } = new List<Produto>();
}