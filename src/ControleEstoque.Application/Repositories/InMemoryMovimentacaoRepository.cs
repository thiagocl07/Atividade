using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Interfaces;

namespace ControleEstoque.Application.Repositories;

public sealed class InMemoryMovimentacaoRepository : IMovimentacaoRepository
{
    private readonly List<Movimentacao> _movimentacoes = new();

    public Movimentacao Salvar(Movimentacao entidade)
    {
        if (entidade is null) throw new ArgumentNullException(nameof(entidade));
        _movimentacoes.Add(entidade);
        return entidade;
    }

    public Movimentacao Atualizar(Movimentacao entidade)
    {
        if (entidade is null) throw new ArgumentNullException(nameof(entidade));
        var index = _movimentacoes.FindIndex(m => m.Id == entidade.Id);
        if (index < 0) throw new InvalidOperationException("Movimentação não encontrada para atualização.");
        _movimentacoes[index] = entidade;
        return entidade;
    }

    public void Deletar(Guid id)
    {
        var m = ObterPorId(id);
        if (m is null) throw new InvalidOperationException("Movimentação não encontrada para exclusão.");
        _movimentacoes.Remove(m);
    }

    public Movimentacao? ObterPorId(Guid id)
    {
        return _movimentacoes.FirstOrDefault(m => m.Id == id);
    }

    public IReadOnlyCollection<Movimentacao> ObterTodos()
    {
        return _movimentacoes.AsReadOnly();
    }

    public IReadOnlyCollection<Movimentacao> ObterPorProduto(Guid produtoId)
    {
        return _movimentacoes.Where(m => m.Produto.Id == produtoId).ToArray();
    }

    public IReadOnlyCollection<Movimentacao> ObterPorPeriodo(DateTime inicio, DateTime fim)
    {
        return _movimentacoes.Where(m => m.DataCriacao >= inicio && m.DataCriacao <= fim).ToArray();
    }
}
