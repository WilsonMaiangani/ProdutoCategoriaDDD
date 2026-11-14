using ProdutoCategoria.Application.DTOs;

namespace ProdutoCategoria.Application.Interfaces;

public interface IProdutoQuery
{
    Task<ProdutoComCategoriaDto?> ObterProdutoComCategoriaAsync(Guid id);
}