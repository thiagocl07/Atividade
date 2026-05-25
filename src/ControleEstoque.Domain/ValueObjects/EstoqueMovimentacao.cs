namespace ControleEstoque.Domain.ValueObjects;

public sealed class EstoqueMovimentacao
{
    public int Quantidade { get; init; }
    public int SaldoAnterior { get; init; }
    public int SaldoAtual { get; init; }

    public EstoqueMovimentacao(int quantidade, int saldoAnterior, int saldoAtual)
    {
        if (quantidade <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));
        if (saldoAnterior < 0) throw new ArgumentException("O saldo anterior não pode ser negativo.", nameof(saldoAnterior));
        if (saldoAtual < 0) throw new ArgumentException("O saldo atual não pode ser negativo.", nameof(saldoAtual));

        Quantidade = quantidade;
        SaldoAnterior = saldoAnterior;
        SaldoAtual = saldoAtual;
    }
}
