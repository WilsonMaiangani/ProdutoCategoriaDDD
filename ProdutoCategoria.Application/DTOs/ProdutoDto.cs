namespace ProdutoCategoria.Application.DTOs;

// DTO Simples
public record ProdutoDto(Guid Id, string Nome, decimal Preco, int Estoque, Guid CategoriaId);

// DTO Composto para a View
public record ProdutoComCategoriaDto(Guid Id, string Nome, decimal Preco, int Estoque, CategoriaDto Categoria);

// DTOs de Entrada
public record CriarProdutoDto(string Nome, decimal Preco, int Estoque, Guid CategoriaId);
public record AtualizarProdutoDto(string Nome, decimal Preco, int Estoque, Guid CategoriaId);