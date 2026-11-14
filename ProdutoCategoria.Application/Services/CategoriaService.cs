using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Application.Mappers;
using ProdutoCategoria.Domain.Entities;
using ProdutoCategoria.Domain.Interfaces;
using ProdutoCategoria.Domain.Interfaces.Repositories;

namespace ProdutoCategoria.Application.Services;

public class CategoriaService
{
    private readonly ICategoriaRepository _repository;

    public CategoriaService(ICategoriaRepository repository) => _repository = repository;

    public async Task<CategoriaDto?> ObterPorIdAsync(Guid id)
        => (await _repository.ObterPorIdAsync(id))?.ToDto();

    public async Task<IReadOnlyList<CategoriaDto>> ObterTodosAsync()
        => (await _repository.ObterTodosAsync()).Select(c => c.ToDto()).ToList();

    public async Task<Guid> CriarAsync(CriarCategoriaDto dto)
    {
        var categoria = new Categoria(dto.Nome, dto.Descricao);
        await _repository.AdicionarAsync(categoria);
        return categoria.Id;
    }

    public async Task AtualizarAsync(Guid id, AtualizarCategoriaDto dto)
    {
        var categoria = await _repository.ObterPorIdAsync(id) ?? throw new Exception("Categoria não encontrada.");
        categoria.AtualizarDados(dto.Nome, dto.Descricao);
        await _repository.AtualizarAsync(categoria);
    }

    public async Task DeletarAsync(Guid id) => await _repository.DeletarAsync(id);
}