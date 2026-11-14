using ProdutoCategoria.Application.DTOs;
using ProdutoCategoria.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ProdutoCategoria.Application.Mappers;

[Mapper]
public static partial class CategoriaMapper
{
    // Gera mapeamento de Entidade para DTO de leitura
    public static partial CategoriaDto ToDto(this Categoria entity);

    // O Mapperly é inteligente, mas como o construtor do domínio tem regras, 
    // é melhor mapear os dados soltos e instanciar na mão no Service, ou usar o mapper apenas para saída.
}