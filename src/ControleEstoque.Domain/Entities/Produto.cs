using ControleEstoque.Domain.Enums;

namespace ControleEstoque.Domain.Entities;

public sealed class Produto
{
    public Guid Id { get; init; }
    public string Codigo { get; private set; }
    public string Nome { get; private set; }
    public Categoria Categoria { get; private set; }
    public Fornecedor Fornecedor { get; private set; }
    public decimal PrecoUnitario { get; private set; }
    public int EstoqueMinimo { get; private set; }
    public int EstoqueAtual { get; private set; }
    public DateTime DataCriacao { get; init; }
    public DateTime DataAtualizacao { get; private set; }

    public Produto(string codigo, string nome, Categoria categoria, Fornecedor fornecedor, decimal precoUnitario, int estoqueMinimo)
    {
        if (string.IsNullOrWhiteSpace(codigo)) throw new ArgumentException("O código do produto é obrigatório.", nameof(codigo));
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));
        if (precoUnitario <= 0) throw new ArgumentException("O preço unitário deve ser maior que zero.", nameof(precoUnitario));
        if (estoqueMinimo < 0) throw new ArgumentException("O estoque mínimo não pode ser negativo.", nameof(estoqueMinimo));

        Id = Guid.NewGuid();
        Codigo = codigo.Trim();
        Nome = nome.Trim();
        Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
        Fornecedor = fornecedor ?? throw new ArgumentNullException(nameof(fornecedor));
        PrecoUnitario = precoUnitario;
        EstoqueMinimo = estoqueMinimo;
        EstoqueAtual = 0;
        DataCriacao = DateTime.UtcNow;
        DataAtualizacao = DataCriacao;
    }

    public void AtualizarPreco(decimal precoUnitario)
    {
        if (precoUnitario <= 0) throw new ArgumentException("O preço unitário deve ser maior que zero.", nameof(precoUnitario));
        PrecoUnitario = precoUnitario;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AtualizarEstoqueMinimo(int estoqueMinimo)
    {
        if (estoqueMinimo < 0) throw new ArgumentException("O estoque mínimo não pode ser negativo.", nameof(estoqueMinimo));
        EstoqueMinimo = estoqueMinimo;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AtualizarDados(string nome, decimal precoUnitario, int estoqueMinimo)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));
        AtualizarPreco(precoUnitario);
        AtualizarEstoqueMinimo(estoqueMinimo);
        Nome = nome.Trim();
        DataAtualizacao = DateTime.UtcNow;
    }

    public void AjustarEstoque(int quantidade)
    {
        if (quantidade < 0) throw new ArgumentException("A quantidade de ajuste deve ser maior ou igual a zero.", nameof(quantidade));
        EstoqueAtual = quantidade;
        DataAtualizacao = DateTime.UtcNow;
    }

    public void RegistrarMovimentacao(TipoMovimentacao tipo, int quantidade)
    {
        if (quantidade <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        var saldoAnterior = EstoqueAtual;
        if (tipo == TipoMovimentacao.Saida && quantidade > EstoqueAtual)
        {
            throw new InvalidOperationException("Quantidade insuficiente em estoque para saída.");
        }

        EstoqueAtual = tipo switch
        {
            TipoMovimentacao.Entrada => EstoqueAtual + quantidade,
            TipoMovimentacao.Saida => EstoqueAtual - quantidade,
            TipoMovimentacao.Ajuste => quantidade,
            _ => EstoqueAtual
        };

        DataAtualizacao = DateTime.UtcNow;
    }
}
