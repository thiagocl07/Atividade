using System;
using System.Collections.Generic;
using System.Linq;
using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Models;
using ControleEstoque.Application.Repositories;
using ControleEstoque.Domain.Entities;
using ControleEstoque.Domain.Interfaces;

namespace ControleEstoque.Application.Services;

public sealed class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    private readonly List<Categoria> _categorias = new();
    private readonly List<Fornecedor> _fornecedores = new();

    public ProdutoService(IProdutoRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        // categorias iniciais
        _categorias.Add(new Categoria("Geral"));
        _categorias.Add(new Categoria("Papelaria"));
        _categorias.Add(new Categoria("Limpeza"));
        _categorias.Add(new Categoria("Carnes"));
        _categorias.Add(new Categoria("Congelados"));
        _categorias.Add(new Categoria("Frios"));
        _categorias.Add(new Categoria("Veterinario"));
        _categorias.Add(new Categoria("Bebidas"));
        _categorias.Add(new Categoria("Padaria"));

        // fornecedores iniciais
        _fornecedores.Add(new Fornecedor("Fornecedor Padrão", "00000000000191"));
        _fornecedores.Add(new Fornecedor("Distribuidora ABC", "12345678000199"));
    }

    public ProdutoModel CriarProduto(string codigo, string nome, string categoriaNome, string fornecedorNome, decimal precoUnitario, int estoqueMinimo)
    {
        if (_repository.ObterPorCodigo(codigo) is not null)
        {
            throw new InvalidOperationException("Já existe um produto com esse código.");
        }

        var categoria = ObterOuCriarCategoria(categoriaNome);
        var fornecedor = ObterOuCriarFornecedor(fornecedorNome);
        var produto = new Produto(codigo, nome, categoria, fornecedor, precoUnitario, estoqueMinimo);

        _repository.Salvar(produto);
        return ProdutoModel.FromEntity(produto);
    }

    public ProdutoModel AtualizarProduto(Guid id, string nome, decimal precoUnitario, int estoqueMinimo)
    {
        var produto = _repository.ObterPorId(id) ?? throw new InvalidOperationException("Produto não encontrado.");

        produto.AtualizarDados(nome, precoUnitario, estoqueMinimo);
        _repository.Atualizar(produto);
        return ProdutoModel.FromEntity(produto);
    }

    public void DeletarProduto(Guid id)
    {
        _repository.Deletar(id);
    }

    public ProdutoModel? ObterProdutoPorId(Guid id)
    {
        var produto = _repository.ObterPorId(id);
        return produto is null ? null : ProdutoModel.FromEntity(produto);
    }

    public IReadOnlyCollection<ProdutoModel> ObterTodos()
    {
        return _repository.ObterTodos().Select(ProdutoModel.FromEntity).ToArray();
    }

    public IReadOnlyCollection<ProdutoModel> ObterPorCodigo(string codigo)
    {
        var produto = _repository.ObterPorCodigo(codigo);
        return produto is null ? Array.Empty<ProdutoModel>() : new[] { ProdutoModel.FromEntity(produto) };
    }

    public IReadOnlyCollection<ProdutoModel> ObterAbaixoDoMinimo()
    {
        return _repository.ObterAbaixoDoMinimo().Select(ProdutoModel.FromEntity).ToArray();
    }

    public IReadOnlyCollection<string> ObterCategorias()
    {
        return _categorias.Select(c => c.Nome).OrderBy(nome => nome).ToArray();
    }

    public IReadOnlyCollection<string> ObterFornecedores()
    {
        return _fornecedores.Select(f => f.Nome).OrderBy(nome => nome).ToArray();
    }

    private Categoria ObterOuCriarCategoria(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            nome = "Geral";
        }

        var categoria = _categorias.FirstOrDefault(c => string.Equals(c.Nome, nome, StringComparison.OrdinalIgnoreCase));
        if (categoria is not null)
        {
            return categoria;
        }

        categoria = new Categoria(nome);
        _categorias.Add(categoria);
        return categoria;
    }

    private Fornecedor ObterOuCriarFornecedor(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            nome = "Fornecedor Padrão";
        }

        var fornecedor = _fornecedores.FirstOrDefault(f => string.Equals(f.Nome, nome, StringComparison.OrdinalIgnoreCase));
        if (fornecedor is not null)
        {
            return fornecedor;
        }

        fornecedor = new Fornecedor(nome, "00000000000191");
        _fornecedores.Add(fornecedor);
        return fornecedor;
    }
}
