using System;

namespace ControleEstoque.Application.Models;

public sealed class MovimentacaoModel
{
    public Guid Id { get; init; }
    public string CodigoProduto { get; init; } = null!;
    public string NomeProduto { get; init; } = null!;
    public string Tipo { get; init; } = null!;
    public int Quantidade { get; init; }
    public int SaldoAnterior { get; init; }
    public int SaldoAtual { get; init; }
    public string Motivo { get; init; } = null!;
    public string UsuarioOperador { get; init; } = null!;
    public DateTime DataCriacao { get; init; }

    public static MovimentacaoModel FromEntity(ControleEstoque.Domain.Entities.Movimentacao m)
    {
        if (m is null) throw new ArgumentNullException(nameof(m));

        return new MovimentacaoModel
        {
            Id = m.Id,
            CodigoProduto = m.Produto.Codigo,
            NomeProduto = m.Produto.Nome,
            Tipo = m.Tipo.ToString(),
            Quantidade = m.Quantidade,
            SaldoAnterior = m.SaldoAnterior,
            SaldoAtual = m.SaldoAtual,
            Motivo = m.Motivo,
            UsuarioOperador = m.UsuarioOperador,
            DataCriacao = m.DataCriacao
        };
    }
}
