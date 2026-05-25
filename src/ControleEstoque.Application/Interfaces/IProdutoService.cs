using System;
using System.Collections.Generic;
using ControleEstoque.Application.Models;

namespace ControleEstoque.Application.Interfaces;

public interface IProdutoService
{
    ProdutoModel CriarProduto(string codigo, string nome, string categoriaNome, string fornecedorNome, decimal precoUnitario, int estoqueMinimo);
    ProdutoModel AtualizarProduto(Guid id, string nome, decimal precoUnitario, int estoqueMinimo);
    void DeletarProduto(Guid id);
    ProdutoModel? ObterProdutoPorId(Guid id);
    IReadOnlyCollection<ProdutoModel> ObterTodos();
    IReadOnlyCollection<ProdutoModel> ObterPorCodigo(string codigo);
    IReadOnlyCollection<ProdutoModel> ObterAbaixoDoMinimo();
}
