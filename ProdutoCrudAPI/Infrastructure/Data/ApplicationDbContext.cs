using Microsoft.EntityFrameworkCore;
using ProdutoCrudAPI.Domain.Models;

namespace ProdutoCrudAPI.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Produto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasForeignKey(p => p.IdCategoria);

        base.OnModelCreating(modelBuilder);
    }
}
