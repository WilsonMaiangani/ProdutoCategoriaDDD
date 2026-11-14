namespace ProdutoCategoria.Infra.Models;

// Classe espelho do retorno do SELECT da SP
public class ProdutoComCategoriaReadModel
{
    public Guid ProdutoId { get; set; }
    public string ProdutoNome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public DateTime ProdutoDataCriacao { get; set; }

    public Guid CategoriaId { get; set; }
    public string CategoriaNome { get; set; } = string.Empty;
    public string? CategoriaDescricao { get; set; }
}