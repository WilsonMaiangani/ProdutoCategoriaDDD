namespace ProdutoCategoria.Domain.Entities;

public class Categoria
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public DateTime DataCriacao { get; private set; }

    private Categoria() { } // Necessário para o EF Core

    public Categoria(string nome, string descricao)
    {
        Id = Guid.NewGuid();
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Descricao = descricao;
        DataCriacao = DateTime.UtcNow;
    }

    // Comportamentos (Mudanças de estado)
    public void AtualizarDados(string nome, string descricao)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Descricao = descricao;
    }
}