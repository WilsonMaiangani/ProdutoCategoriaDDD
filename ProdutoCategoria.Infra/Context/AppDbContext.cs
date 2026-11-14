using Microsoft.EntityFrameworkCore;
using ProdutoCategoria.Domain.Entities;


namespace ProdutoCategoria.Infra.Context;

public class AppDbContext : DbContext
{
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Produto> Produtos => Set<Produto>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Como usamos SPs, não mapeamos para tabelas reais via modelBuilder.ToTable()
        // Mas precisamos dizer ao EF qual é a chave primária para que ele consiga
        // materializar o resultado do FromSqlRaw nas entidades corretamente.

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            // Ignora propriedades que as SPs não retornarem, se houver
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(p => p.Categoria).WithMany().HasForeignKey(p => p.CategoriaId);
        });
    }
}