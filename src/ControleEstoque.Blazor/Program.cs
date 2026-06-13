using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Repositories;
using ControleEstoque.Application.Services;
using ControleEstoque.Domain.Interfaces;
using ControleEstoque.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register SignalR to support real-time communication for interactive components
builder.Services.AddSignalR();

// Registrar repositórios e serviços em memória compartilhados
var produtoRepository = new InMemoryProdutoRepository();
var movimentacaoRepository = new InMemoryMovimentacaoRepository();

// Instanciar serviços para permitir seed de dados em memória
var produtoService = new ProdutoService(produtoRepository);

// Produtos iniciais (seed)
produtoService.CriarProduto("CARN-001", "Filé de frango congelado 1kg", "Carnes", "Distribuidora ABC", 22.50m, 8);
produtoService.CriarProduto("CONG-001", "Pizza congelada sabor mussarela", "Congelados", "Distribuidora ABC", 15.00m, 12);
produtoService.CriarProduto("FRI-001", "Queijo mussarela 200g", "Frios", "Fornecedor Padrão", 11.80m, 5);
produtoService.CriarProduto("VET-001", "Ração para cachorro 2kg", "Veterinario", "Distribuidora ABC", 42.00m, 4);
produtoService.CriarProduto("BEB-001", "Refrigerante 2L", "Bebidas", "Fornecedor Padrão", 10.00m, 20);
produtoService.CriarProduto("PAD-001", "Pão francês pacote 10un", "Padaria", "Fornecedor Padrão", 13.50m, 15);
produtoService.CriarProduto("PAP-002", "Folhas A4 pacote 100", "Papelaria", "Fornecedor Padrão", 18.00m, 2);

var movimentacaoService = new MovimentacaoService(movimentacaoRepository, produtoRepository);

// Registrar repositórios como singletons para garantir persistência em memória
builder.Services.AddSingleton<IProdutoRepository>(_ => produtoRepository);
builder.Services.AddSingleton(produtoRepository);

// Registrar repositórios e instâncias de serviços seedadas
builder.Services.AddSingleton<ControleEstoque.Domain.Interfaces.IMovimentacaoRepository>(_ => movimentacaoRepository);
builder.Services.AddSingleton(movimentacaoRepository);

builder.Services.AddSingleton<IProdutoService>(_ => produtoService);
builder.Services.AddSingleton(produtoService);

builder.Services.AddSingleton<IMovimentacaoService>(_ => movimentacaoService);
builder.Services.AddSingleton(movimentacaoService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.MapStaticAssets();

// Enable routing and WebSockets for SignalR/interactive server components
app.UseRouting();

// Enable WebSockets for SignalR before antiforgery/endpoints
app.UseWebSockets();

// Antiforgery middleware must be registered after UseRouting and before endpoints
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
