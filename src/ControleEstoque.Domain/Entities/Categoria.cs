namespace ControleEstoque.Domain.Entities;

public sealed class Categoria
{
    public Guid Id { get; init; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public DateTime DataCriacao { get; init; }
    public DateTime DataAtualizacao { get; private set; }

    public Categoria(string nome, string? descricao = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome da categoria é obrigatório.", nameof(nome));
        }

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Descricao = descricao?.Trim();
        DataCriacao = DateTime.UtcNow;
        DataAtualizacao = DataCriacao;
    }

    public void Atualizar(string nome, string? descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome da categoria é obrigatório.", nameof(nome));
        }

        Nome = nome.Trim();
        Descricao = descricao?.Trim();
        DataAtualizacao = DateTime.UtcNow;
    }
}
