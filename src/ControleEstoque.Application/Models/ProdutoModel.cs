using System;
using ControleEstoque.Domain.Entities;

namespace ControleEstoque.Application.Models;

public sealed class ProdutoModel
{
    public Guid Id { get; init; }
    public string Codigo { get; init; } = null!;
    public string Nome { get; init; } = null!;
    public string Categoria { get; init; } = null!;
    public string Fornecedor { get; init; } = null!;
    public decimal PrecoUnitario { get; init; }
    public int EstoqueMinimo { get; init; }
    public int EstoqueAtual { get; init; }
    public DateTime DataCriacao { get; init; }
    public DateTime DataAtualizacao { get; init; }

    public static ProdutoModel FromEntity(Produto produto)
    {
        return new ProdutoModel
        {
            Id = produto.Id,
            Codigo = produto.Codigo,
            Nome = produto.Nome,
            Categoria = produto.Categoria.Nome,
            Fornecedor = produto.Fornecedor.Nome,
            PrecoUnitario = produto.PrecoUnitario,
            EstoqueMinimo = produto.EstoqueMinimo,
            EstoqueAtual = produto.EstoqueAtual,
            DataCriacao = produto.DataCriacao,
            DataAtualizacao = produto.DataAtualizacao
        };
    }
}
