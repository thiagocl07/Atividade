using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Interfaces;

namespace ControleEstoque.Application.Repositories;

public sealed class InMemoryProdutoRepository : IProdutoRepository
{
    private readonly List<Produto> _produtos = new();

    public Produto Salvar(Produto entidade)
    {
        if (entidade is null)
        {
            throw new ArgumentNullException(nameof(entidade));
        }

        _produtos.Add(entidade);
        return entidade;
    }

    public Produto Atualizar(Produto entidade)
    {
        if (entidade is null)
        {
            throw new ArgumentNullException(nameof(entidade));
        }

        var index = _produtos.FindIndex(p => p.Id == entidade.Id);
        if (index < 0)
        {
            throw new InvalidOperationException("Produto não encontrado para atualização.");
        }

        _produtos[index] = entidade;
        return entidade;
    }

    public void Deletar(Guid id)
    {
        var produto = ObterPorId(id);
        if (produto is null)
        {
            throw new InvalidOperationException("Produto não encontrado para exclusão.");
        }

        _produtos.Remove(produto);
    }

    public Produto? ObterPorId(Guid id)
    {
        return _produtos.FirstOrDefault(p => p.Id == id);
    }

    public IReadOnlyCollection<Produto> ObterTodos()
    {
        return _produtos.AsReadOnly();
    }

    public Produto? ObterPorCodigo(string codigo)
    {
        return _produtos.FirstOrDefault(p => string.Equals(p.Codigo, codigo, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyCollection<Produto> ObterPorCategoria(Guid categoriaId)
    {
        return _produtos.Where(p => p.Categoria.Id == categoriaId).ToArray();
    }

    public IReadOnlyCollection<Produto> ObterAbaixoDoMinimo()
    {
        return _produtos.Where(p => p.EstoqueAtual < p.EstoqueMinimo).ToArray();
    }

    public IReadOnlyCollection<Produto> ObterPorFornecedor(Guid fornecedorId)
    {
        return _produtos.Where(p => p.Fornecedor.Id == fornecedorId).ToArray();
    }
}
