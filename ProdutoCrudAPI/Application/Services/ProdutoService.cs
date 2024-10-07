using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Domain.Models;
using ProdutoCrudAPI.Domain.Repositories;

namespace ProdutoCrudAPI.Application.Services;
public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
    }
    
    public ProdutoDto CreateProduto(ProdutoCreateDto produtoAddDto)
    {
        var produto = _mapper.Map<Produto>(produtoAddDto);
        _produtoRepository.Create(produto);

        return _mapper.Map<ProdutoDto>(produto);
    }

    public void Delete(int id)
    {
        _produtoRepository.Delete(id);
    }

    public IEnumerable<ProdutoDto> GetAll(int skip, int take, string? nome, string? descricao)
    {
        var produtos = _produtoRepository.GetAll(skip, take, nome, descricao);
        var produtosDto = produtos.Select(produto => _mapper.Map<ProdutoDto>(produto));

        return produtosDto;
    }    

    public ProdutoDto GetById(int id)
    {
        var produto = _produtoRepository.GetById(id);
        return _mapper.Map<ProdutoDto>(produto);
    }

    public ProdutoDto Update(int id, ProdutoUpdateDto produtoUpdateDto)
    {
        var produto = _produtoRepository.GetById(id);
        
        if (produto is null) return null;

        produto.atualizarInformacoes(produtoUpdateDto);        
        _produtoRepository.Update(produto);

        return _mapper.Map<ProdutoDto>(produto);
    }
}
