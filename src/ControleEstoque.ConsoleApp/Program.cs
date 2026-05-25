using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Models;
using ControleEstoque.Application.Repositories;
using ControleEstoque.Application.Services;

IProdutoService produtoService = new ProdutoService(new InMemoryProdutoRepository());

while (true)
{
    ExibirMenu();
    var opcao = Console.ReadLine();

    if (string.Equals(opcao, "0", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    try
    {
        ProcessarOpcao(opcao, produtoService);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }

    Console.WriteLine("\nPressione Enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}

static void ExibirMenu()
{
    Console.WriteLine("==== Controle de Estoque ====");
    Console.WriteLine("1 - Criar produto");
    Console.WriteLine("2 - Listar produtos");
    Console.WriteLine("3 - Buscar produto por ID");
    Console.WriteLine("4 - Buscar produto por código");
    Console.WriteLine("5 - Atualizar produto");
    Console.WriteLine("6 - Excluir produto");
    Console.WriteLine("7 - Produtos abaixo do estoque mínimo");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
}

static void ProcessarOpcao(string? opcao, IProdutoService produtoService)
{
    switch (opcao)
    {
        case "1": CriarProduto(produtoService); break;
        case "2": ListarProdutos(produtoService); break;
        case "3": BuscarProdutoPorId(produtoService); break;
        case "4": BuscarProdutoPorCodigo(produtoService); break;
        case "5": AtualizarProduto(produtoService); break;
        case "6": ExcluirProduto(produtoService); break;
        case "7": ListarProdutosAbaixoDoMinimo(produtoService); break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}

static void CriarProduto(IProdutoService produtoService)
{
    Console.WriteLine("-- Criar produto --");
    var codigo = LerTexto("Código");
    var nome = LerTexto("Nome");
    var categoria = LerTexto("Categoria");
    var fornecedor = LerTexto("Fornecedor");
    var preco = LerDecimal("Preço unitário");
    var minimo = LerInteiro("Estoque mínimo");

    var produto = produtoService.CriarProduto(codigo, nome, categoria, fornecedor, preco, minimo);
    ExibirProduto(produto);
}

static void ListarProdutos(IProdutoService produtoService)
{
    Console.WriteLine("-- Produtos cadastrados --");
    var produtos = produtoService.ObterTodos();
    ExibirLista(produtos);
}

static void BuscarProdutoPorId(IProdutoService produtoService)
{
    Console.WriteLine("-- Buscar produto por ID --");
    var id = LerGuid("ID do produto");
    var produto = produtoService.ObterProdutoPorId(id);
    if (produto is null)
    {
        Console.WriteLine("Produto não encontrado.");
        return;
    }

    ExibirProduto(produto);
}

static void BuscarProdutoPorCodigo(IProdutoService produtoService)
{
    Console.WriteLine("-- Buscar produto por código --");
    var codigo = LerTexto("Código");
    var produtos = produtoService.ObterPorCodigo(codigo);
    ExibirLista(produtos);
}

static void AtualizarProduto(IProdutoService produtoService)
{
    Console.WriteLine("-- Atualizar produto --");
    var id = LerGuid("ID do produto");
    var nome = LerTexto("Novo nome");
    var preco = LerDecimal("Novo preço unitário");
    var minimo = LerInteiro("Novo estoque mínimo");

    var produto = produtoService.AtualizarProduto(id, nome, preco, minimo);
    Console.WriteLine("Produto atualizado com sucesso.");
    ExibirProduto(produto);
}

static void ExcluirProduto(IProdutoService produtoService)
{
    Console.WriteLine("-- Excluir produto --");
    var id = LerGuid("ID do produto");
    produtoService.DeletarProduto(id);
    Console.WriteLine("Produto excluído com sucesso.");
}

static void ListarProdutosAbaixoDoMinimo(IProdutoService produtoService)
{
    Console.WriteLine("-- Produtos abaixo do estoque mínimo --");
    var produtos = produtoService.ObterAbaixoDoMinimo();
    ExibirLista(produtos);
}

static void ExibirLista(IReadOnlyCollection<ProdutoModel> produtos)
{
    if (!produtos.Any())
    {
        Console.WriteLine("Nenhum produto encontrado.");
        return;
    }

    foreach (var produto in produtos)
    {
        ExibirProduto(produto);
        Console.WriteLine(new string('-', 40));
    }
}

static void ExibirProduto(ProdutoModel produto)
{
    Console.WriteLine($"ID: {produto.Id}");
    Console.WriteLine($"Código: {produto.Codigo}");
    Console.WriteLine($"Nome: {produto.Nome}");
    Console.WriteLine($"Categoria: {produto.Categoria}");
    Console.WriteLine($"Fornecedor: {produto.Fornecedor}");
    Console.WriteLine($"Preço unitário: {produto.PrecoUnitario:C}");
    Console.WriteLine($"Estoque mínimo: {produto.EstoqueMinimo}");
    Console.WriteLine($"Estoque atual: {produto.EstoqueAtual}");
    Console.WriteLine($"Criado em: {produto.DataCriacao:yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine($"Atualizado em: {produto.DataAtualizacao:yyyy-MM-dd HH:mm:ss}");
}

static string LerTexto(string etiqueta)
{
    Console.Write($"{etiqueta}: ");
    var valor = Console.ReadLine();
    return string.IsNullOrWhiteSpace(valor) ? string.Empty : valor.Trim();
}

static decimal LerDecimal(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        if (decimal.TryParse(Console.ReadLine(), out var valor) && valor > 0)
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Digite um número maior que zero.");
    }
}

static int LerInteiro(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        if (int.TryParse(Console.ReadLine(), out var valor) && valor >= 0)
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Digite um número inteiro maior ou igual a zero.");
    }
}

static Guid LerGuid(string etiqueta)
{
    while (true)
    {
        Console.Write($"{etiqueta}: ");
        var entrada = Console.ReadLine();
        if (Guid.TryParse(entrada, out var id))
        {
            return id;
        }

        Console.WriteLine("ID inválido. Digite um GUID válido.");
    }
}
