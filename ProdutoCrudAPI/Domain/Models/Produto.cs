using ProdutoCrudAPI.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ProdutoCrudAPI.Domain.Models;
public class Produto
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Descricao { get; set; }

    [Required]
    public double Preco { get; set; }

    [Required]
    public DateTime DataValidade { get; set; }

    [Required]
    public string Imagem { get; set; }

    [Required]
    public int IdCategoria { get; set; }

    public Categoria Categoria { get; set; }

    public void atualizarInformacoes(ProdutoUpdateDto dadosProdutoDto)
    {
        if (!string.IsNullOrEmpty(dadosProdutoDto.Nome))
        {
            Nome = dadosProdutoDto.Nome;
        }

        if (!string.IsNullOrEmpty(dadosProdutoDto.Descricao))
        {
            Descricao = dadosProdutoDto.Descricao;
        }

        if (dadosProdutoDto.Preco.HasValue)
        {
            Preco = dadosProdutoDto.Preco.Value;
        }

        if (dadosProdutoDto.DataValidade.HasValue)
        {
            DataValidade = dadosProdutoDto.DataValidade.Value;
        }

        if (!string.IsNullOrEmpty(dadosProdutoDto.Imagem))
        {
            Imagem = dadosProdutoDto.Imagem;
        }

        if (dadosProdutoDto.Categoria is not null && !string.IsNullOrEmpty(dadosProdutoDto.Categoria.Nome))
        {
            Categoria.Nome = dadosProdutoDto.Categoria.Nome;
        }
    }
}
