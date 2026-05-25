namespace ControleEstoque.Domain.Entities;

public sealed class Fornecedor
{
    public Guid Id { get; init; }
    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public string? Telefone { get; private set; }
    public string? Email { get; private set; }
    public string? Endereco { get; private set; }
    public DateTime DataCriacao { get; init; }

    public Fornecedor(string nome, string cnpj, string? telefone = null, string? email = null, string? endereco = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("O nome do fornecedor é obrigatório.", nameof(nome));
        }

        if (string.IsNullOrWhiteSpace(cnpj))
        {
            throw new ArgumentException("O CNPJ do fornecedor é obrigatório.", nameof(cnpj));
        }

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Cnpj = cnpj.Trim();
        Telefone = string.IsNullOrWhiteSpace(telefone) ? null : telefone.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Endereco = string.IsNullOrWhiteSpace(endereco) ? null : endereco.Trim();
        DataCriacao = DateTime.UtcNow;
    }

    public void Atualizar(string nome, string cnpj, string? telefone = null, string? email = null, string? endereco = null)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do fornecedor é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(cnpj)) throw new ArgumentException("O CNPJ do fornecedor é obrigatório.", nameof(cnpj));

        Nome = nome.Trim();
        Cnpj = cnpj.Trim();
        Telefone = string.IsNullOrWhiteSpace(telefone) ? null : telefone.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();
        Endereco = string.IsNullOrWhiteSpace(endereco) ? null : endereco.Trim();
    }
}
