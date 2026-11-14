using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProdutoCategoria.Application.Mappers;

[Mapper]
public static partial class ProdutoMapper
{
    public static partial ProdutoDto ToDto(this Produto entity);
}