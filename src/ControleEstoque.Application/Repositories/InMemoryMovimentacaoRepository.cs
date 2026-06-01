using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Models;

namespace ControleEstoque.Application.Repositories;

public sealed class InMemoryMovimentacaoRepository
{
    private readonly List<MovimentacaoModel> _movimentacoes = new();

    public void Registrar(MovimentacaoModel movimentacao)
    {
        if (movimentacao is null)
        {
            throw new ArgumentNullException(nameof(movimentacao));
        }

        _movimentacoes.Add(movimentacao);
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterTodas()
    {
        return _movimentacoes.AsReadOnly();
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterPorProduto(string codigoProduto)
    {
        return _movimentacoes
            .Where(m => string.Equals(m.CodigoProduto, codigoProduto, StringComparison.OrdinalIgnoreCase))
            .ToArray();
    }

    public IReadOnlyCollection<MovimentacaoModel> ObterRecentes(int quantidade = 50)
    {
        return _movimentacoes
            .OrderByDescending(m => m.DataCriacao)
            .Take(quantidade)
            .ToArray();
    }
}
