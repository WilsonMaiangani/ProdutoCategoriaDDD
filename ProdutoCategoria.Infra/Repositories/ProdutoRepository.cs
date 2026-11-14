using Microsoft.EntityFrameworkCore;
using ProdutoCategoria.Domain.Entities;
using ProdutoCategoria.Domain.Interfaces;
using ProdutoCategoria.Domain.Interfaces.Repositories;
using ProdutoCategoria.Infra.Context;

namespace ProdutoCategoria.Infra.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context) => _context = context;

    public async Task<Produto?> ObterPorIdAsync(Guid id)
    {
        var result = await _context.Produtos
            .FromSqlInterpolated($"CALL sp_ObterProdutoPorId({id})")
            .ToListAsync();
        return result.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Produto>> ObterTodosAsync()
    {
        return await _context.Produtos
            .FromSqlRaw("CALL sp_ListarProdutos()")
            .ToListAsync();
    }

    public async Task AdicionarAsync(Produto p)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"CALL sp_CriarProduto({p.Id}, {p.Nome}, {p.Preco}, {p.Estoque}, {p.CategoriaId}, {p.DataCriacao})");
    }

    public async Task AtualizarAsync(Produto p)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"CALL sp_AtualizarProduto({p.Id}, {p.Nome}, {p.Preco}, {p.Estoque}, {p.CategoriaId})");
    }

    public async Task DeletarAsync(Guid id)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_DeletarProduto({id})");
    }

    public async Task<Produto?> ObterPorIdAsync2(Guid id)
    {
        var t = await _context.Produtos
                .FromSqlRaw("CALL sp_ObterProdutoComCategoria(@p0)", id)
                .Include(p => p.Categoria) // Inclui a categoria na leitura
                .AsNoTracking()
                .ToListAsync();

        return t.FirstOrDefault();
    }
}