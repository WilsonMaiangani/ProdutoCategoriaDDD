using Microsoft.EntityFrameworkCore;
using ProdutoCategoria.Domain.Entities;
using ProdutoCategoria.Domain.Interfaces.Repositories;
using ProdutoCategoria.Infra.Context;

namespace ProdutoCategoria.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> ObterPorIdAsync(Guid id)
    {
        // 1. Executa a SP e traz o resultado para a memória da aplicação
        var resultado = await _context.Categorias
            .FromSqlInterpolated($"CALL sp_ObterCategoriaPorId({id})")
            .AsNoTracking()
            .ToListAsync();

        // 2. Pega o primeiro (ou nulo) na memória, sem gerar SQL errado
        return resultado.FirstOrDefault();
        //var t= await _context.Categorias
        //       .FromSqlRaw("CALL SpObterCategoriaPorId(@p0)", id)
        //       .AsNoTracking()
        //       .FirstOrDefaultAsync();
        //return t;
    }

    public async Task<IReadOnlyList<Categoria>> ObterTodosAsync()
    {
        return await _context.Categorias
            .FromSqlRaw("CALL sp_ListarCategorias()")
            .ToListAsync();
    }

    public async Task AdicionarAsync(Categoria categoria)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"CALL sp_CriarCategoria({categoria.Id}, {categoria.Nome}, {categoria.Descricao}, {categoria.DataCriacao})");
    }

    public async Task AtualizarAsync(Categoria categoria)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync(
            $"CALL sp_AtualizarCategoria({categoria.Id}, {categoria.Nome}, {categoria.Descricao})");
    }

    public async Task DeletarAsync(Guid id)
    {
        await _context.Database.ExecuteSqlInterpolatedAsync($"CALL sp_DeletarCategoria({id})");
    }
}