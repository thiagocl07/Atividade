using ControleEstoque.Application.Interfaces;
using ControleEstoque.Application.Repositories;
using ControleEstoque.Application.Services;
using ControleEstoque.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registrar repositórios e serviços em memória compartilhados
var movimentacaoRepository = new InMemoryMovimentacaoRepository();
builder.Services.AddSingleton<IProdutoService>(_ => new ProdutoService(new InMemoryProdutoRepository()));
builder.Services.AddSingleton<IMovimentacaoService>(_ => new MovimentacaoService(movimentacaoRepository));
builder.Services.AddSingleton(movimentacaoRepository);

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

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
