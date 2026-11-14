using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Application.Mappers;
using ProdutoCategoria.Domain.Entities;
using ProdutoCategoria.Domain.Interfaces;
using ProdutoCategoria.Domain.Interfaces.Repositories;

namespace ProdutoCategoria.Application.Services;

public class ProdutoService
{
    private readonly IProdutoRepository _produtoRepo;
    private readonly ICategoriaRepository _categoriaRepo;

    public ProdutoService(IProdutoRepository produtoRepo, ICategoriaRepository categoriaRepo)
    {
        _produtoRepo = produtoRepo;
        _categoriaRepo = categoriaRepo;
    }

    public async Task<ProdutoDto?> ObterPorIdAsync(Guid id)
        => (await _produtoRepo.ObterPorIdAsync(id))?.ToDto();

    public async Task<IReadOnlyList<ProdutoDto>> ObterTodosAsync()
        => (await _produtoRepo.ObterTodosAsync()).Select(p => p.ToDto()).ToList();

    public async Task<ProdutoComCategoriaDto?> ObterComCategoriaAsync(Guid id)
    {
        // 1. Busca o Agregado Produto
        var produto = await _produtoRepo.ObterPorIdAsync(id);
        if (produto is null) return null;

        // 2. Busca o Agregado Categoria separadamente
        var categoria = await _categoriaRepo.ObterPorIdAsync(produto.CategoriaId);

        // 3. Compõe o DTO de forma explícita e limpa
        return new ProdutoComCategoriaDto(
            produto.Id,
            produto.Nome,
            produto.Preco,
            produto.Estoque,
            categoria?.ToDto()! // Usa o mapper da categoria
        );
    }

    public async Task<Guid> CriarAsync(CriarProdutoDto dto)
    {
        var produto = new Produto(dto.Nome, dto.Preco, dto.Estoque, dto.CategoriaId);
        await _produtoRepo.AdicionarAsync(produto);
        return produto.Id;
    }

    public async Task AtualizarAsync(Guid id, AtualizarProdutoDto dto)
    {
        var produto = await _produtoRepo.ObterPorIdAsync(id) ?? throw new Exception("Produto não encontrado.");
        produto.AtualizarInformacoes(dto.Nome, dto.Preco, dto.Estoque, dto.CategoriaId);
        await _produtoRepo.AtualizarAsync(produto);
    }

    public async Task DeletarAsync(Guid id) => await _produtoRepo.DeletarAsync(id);
}