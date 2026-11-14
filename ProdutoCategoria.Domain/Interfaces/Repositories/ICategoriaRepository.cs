using ProdutoCategoria.Domain.Entities;

namespace ProdutoCategoria.Domain.Interfaces.Repositories;

public interface ICategoriaRepository
{
    Task<Categoria?> ObterPorIdAsync(Guid id);
    Task<IReadOnlyList<Categoria>> ObterTodosAsync();
    Task AdicionarAsync(Categoria categoria);
    Task AtualizarAsync(Categoria categoria);
    Task DeletarAsync(Guid id);
}