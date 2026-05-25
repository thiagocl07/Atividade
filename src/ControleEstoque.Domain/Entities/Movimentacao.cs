using ControleEstoque.Domain.Enums;

namespace ControleEstoque.Domain.Entities;

public sealed class Movimentacao
{
    public Guid Id { get; init; }
    public Produto Produto { get; private set; }
    public TipoMovimentacao Tipo { get; private set; }
    public int Quantidade { get; private set; }
    public int SaldoAnterior { get; private set; }
    public int SaldoAtual { get; private set; }
    public string Motivo { get; private set; }
    public string UsuarioOperador { get; private set; }
    public DateTime DataCriacao { get; init; }

    public Movimentacao(Produto produto, TipoMovimentacao tipo, int quantidade, int saldoAnterior, string motivo, string usuarioOperador)
    {
        Produto = produto ?? throw new ArgumentNullException(nameof(produto));
        Tipo = tipo;
        if (quantidade <= 0) throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));
        Quantidade = quantidade;
        SaldoAnterior = saldoAnterior;
        SaldoAtual = CalcularSaldoAtual(tipo, saldoAnterior, quantidade);
        Motivo = string.IsNullOrWhiteSpace(motivo) ? throw new ArgumentException("O motivo é obrigatório.", nameof(motivo)) : motivo.Trim();
        UsuarioOperador = string.IsNullOrWhiteSpace(usuarioOperador) ? throw new ArgumentException("O usuário operador é obrigatório.", nameof(usuarioOperador)) : usuarioOperador.Trim();
        DataCriacao = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }

    private static int CalcularSaldoAtual(TipoMovimentacao tipo, int saldoAnterior, int quantidade) => tipo switch
    {
        TipoMovimentacao.Entrada => saldoAnterior + quantidade,
        TipoMovimentacao.Saida when quantidade > saldoAnterior => throw new InvalidOperationException("Quantidade maior que o saldo disponível."),
        TipoMovimentacao.Saida => saldoAnterior - quantidade,
        TipoMovimentacao.Ajuste => quantidade,
        _ => saldoAnterior
    };
}
