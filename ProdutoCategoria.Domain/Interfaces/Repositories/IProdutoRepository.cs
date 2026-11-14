using ProdutoCategoria.Domain.Entities;

namespace ProdutoCategoria.Domain.Interfaces.Repositories;

public interface IProdutoRepository
{
    Task<Produto?> ObterPorIdAsync(Guid id);
    Task<IReadOnlyList<Produto>> ObterTodosAsync();
    Task AdicionarAsync(Produto produto);
    Task AtualizarAsync(Produto produto);
    Task DeletarAsync(Guid id);


}