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
}
