using System;
using ControleEstoque.Application.Repositories;
using ControleEstoque.Application.Services;

Console.WriteLine("Iniciando teste de persistência...");

var repo = new InMemoryProdutoRepository();
var service = new ProdutoService(repo);

var produtosAntes = service.ObterTodos();
Console.WriteLine($"Produtos antes: {produtosAntes.Count}");

try
{
	var p = service.CriarProduto("PRD-TEST", "Produto Teste", "Geral", "Fornecedor Teste", 10.50m, 5);
	Console.WriteLine($"Criado: {p.Codigo} - {p.Nome}");
}
catch (Exception ex)
{
	Console.WriteLine($"Erro ao criar produto: {ex.Message}");
}

var produtosDepois = service.ObterTodos();
Console.WriteLine($"Produtos depois: {produtosDepois.Count}");

foreach (var prod in produtosDepois)
{
	Console.WriteLine($"- {prod.Id} {prod.Codigo} {prod.Nome} Min:{prod.EstoqueMinimo} Atual:{prod.EstoqueAtual}");
}
