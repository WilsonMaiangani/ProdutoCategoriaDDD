namespace ProdutoCategoria.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }
    public Guid CategoriaId { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public Categoria? Categoria { get; private set; }

    private Produto() { } // Para o EF Core

    public Produto(string nome, decimal preco, int estoque, Guid categoriaId)
    {
        Id = Guid.NewGuid();
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        AtualizarPreco(preco);
        AtualizarEstoque(estoque);
        CategoriaId = categoriaId;
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarInformacoes(string nome, decimal preco, int estoque, Guid categoriaId)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        AtualizarPreco(preco);
        AtualizarEstoque(estoque);
        CategoriaId = categoriaId;
    }

    public void AtualizarPreco(decimal preco)
    {
        if (preco < 0) throw new ArgumentException("Preço não pode ser negativo.");
        Preco = preco;
    }

    public void AtualizarEstoque(int estoque)
    {
        if (estoque < 0) throw new ArgumentException("Estoque não pode ser negativo.");
        Estoque = estoque;
    }
}

