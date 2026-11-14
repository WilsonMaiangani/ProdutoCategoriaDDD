using System;
using System.Collections.Generic;
using System.Text;

namespace ProdutoCategoria.Application.DTOs
{
    public record CategoriaDto(Guid Id, string Nome, string? Descricao, DateTime DataCriacao);
    public record CriarCategoriaDto(string Nome, string? Descricao);
    public record AtualizarCategoriaDto(string Nome, string? Descricao);
}
