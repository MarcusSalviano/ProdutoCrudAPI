using AutoMapper;
using ProdutoCrudAPI.Application.Dtos;
using ProdutoCrudAPI.Domain.Models;

namespace ProdutoCrudAPI.Infrastructure.Mapping.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoCreateDto, Produto>();
            CreateMap<ProdutoUpdateDto, Produto>();
            CreateMap<Produto, ProdutoDto>();
            CreateMap<CategoriaCreateDto, Categoria>();
            CreateMap<Categoria, CategoriaDto>();
        }
    }
}