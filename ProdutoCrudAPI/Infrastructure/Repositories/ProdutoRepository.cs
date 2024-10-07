using Microsoft.EntityFrameworkCore;
using ProdutoCrudAPI.Domain.Models;
using ProdutoCrudAPI.Domain.Repositories;
using ProdutoCrudAPI.Infrastructure.Data;

namespace ProdutoCrudAPI.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
       
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Produto GetById(int id)
        {
            return _context.Produtos.Include(c => c.Categoria).FirstOrDefault(produto => produto.Id == id);
        }

        public IEnumerable<Produto> GetAll(int skip, int take, string? nome, string? descricao)
        {
            var query = _context.Produtos.Include(c => c.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                query = query.Where(p => p.Descricao.Contains(descricao));
            }

            return query.Skip(skip).Take(take).ToList();     
        }

        public void Create(Produto produto)
        {
            // Verifica se a categoria já existe no banco de dados
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.Nome == produto.Categoria.Nome);

            // Se a categoria não existir, cria uma nova
            if (categoria is null)
            {
                categoria = new Categoria { Nome = produto.Categoria.Nome };
                _context.Categorias.Add(categoria);
                _context.SaveChanges(); // Salva a nova categoria para obter o ID
            }

            produto.IdCategoria = categoria.Id;
            produto.Categoria = categoria;

            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }


        public void Update(Produto produto)
        {
            var produtoDb = _context.Produtos.Include(p => p.Categoria).FirstOrDefault(p => p.Id == produto.Id);

            // Desanexar a categoria existente para evitar problemas de estado
            _context.Entry(produto.Categoria).State = EntityState.Detached;

            // Verifica se a categoria já existe
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.Nome == produto.Categoria.Nome);

            if (categoria is null)
            {
                // Cria uma nova categoria se não existir
                categoria = new Categoria { Nome = produto.Categoria.Nome };
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
            }

            // Atualiza a referência da categoria no produto
            produto.Categoria = categoria;
            produto.IdCategoria = categoria.Id;

            // Marca o produto como modificado e salva as mudanças
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cliente = _context.Produtos.Find(id);
            if (cliente is not null)
            {
                _context.Produtos.Remove(cliente);
                _context.SaveChanges();
            }
        }        
    }
}
